﻿using DatabaseInterpreter.Core;
using DatabaseInterpreter.Model;
using DatabaseInterpreter.Utility;
using DatabaseManager.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;


namespace DatabaseManager.Core
{
    public class ScriptRunner
    {
        private DbTransaction transaction = null;
        private bool cancelRequested = false;
        private IObserver<FeedbackInfo> observer;
        private bool isBusy = false;

        public CancellationTokenSource CancellationTokenSource { get; private set; }
        public bool CancelRequested => this.cancelRequested;
        public bool IsBusy => this.isBusy;      
        public int LimitCount { get; set; } = 1000;

        public event FeedbackHandler OnFeedback;

        public ScriptRunner()
        {
            this.CancellationTokenSource = new CancellationTokenSource();
        }

        public void Subscribe(IObserver<FeedbackInfo> observer)
        {
            this.observer = observer;
        }

        public async Task<QueryResult> Run(DatabaseType dbType, ConnectionInfo connectionInfo, string script)
        {
            this.cancelRequested = false;
            this.isBusy = false;

            QueryResult result = new QueryResult();

            DbInterpreterOption option = new DbInterpreterOption() { RequireInfoMessage = true };

            DbInterpreter dbInterpreter = DbInterpreterHelper.GetDbInterpreter(dbType, connectionInfo, option);

            dbInterpreter.Subscribe(this.observer);

            try
            {
                ScriptParser scriptParser = new ScriptParser(dbInterpreter, script);

                string cleanScript = scriptParser.CleanScript;

                if (string.IsNullOrEmpty(cleanScript))
                {
                    result.DoNothing = true;
                    return result;
                }

                using (DbConnection dbConnection = dbInterpreter.CreateConnection())
                {
                    if (scriptParser.IsSelect())
                    {
                        this.isBusy = true;
                        result.ResultType = QueryResultType.Grid;

                        if (!scriptParser.IsCreateOrAlterScript() && dbInterpreter.ScriptsDelimiter.Length == 1)
                        {
                            cleanScript = script.TrimEnd(dbInterpreter.ScriptsDelimiter[0]);
                        }

                        DataTable dataTable = await dbInterpreter.GetDataTableAsync(dbConnection, script, this.LimitCount);

                        result.Result = dataTable;
                    }
                    else
                    {
                        this.isBusy = true;
                        result.ResultType = QueryResultType.Text;

                        await dbConnection.OpenAsync();

                        this.transaction = await dbConnection.BeginTransactionAsync();

                        IEnumerable<string> commands = Enumerable.Empty<string>();

                        if (scriptParser.IsCreateOrAlterScript())
                        {
                            if (dbInterpreter.DatabaseType == DatabaseType.Oracle)
                            {
                                script = script.Trim().TrimEnd(dbInterpreter.ScriptsDelimiter[0]);
                            }

                            commands = new string[] { script };
                        }
                        else
                        {
                            string delimiter = dbInterpreter.ScriptsDelimiter;                         

                            commands = script.Split(new string[] { delimiter, delimiter.Replace("\r", "\n") }, StringSplitOptions.RemoveEmptyEntries);
                        }

                        int affectedRows = 0;

                        foreach (string command in commands)
                        {
                            if (string.IsNullOrEmpty(command.Trim()))
                            {
                                continue;
                            }

                            CommandInfo commandInfo = new CommandInfo()
                            {
                                CommandType = CommandType.Text,
                                CommandText = command,
                                Transaction = this.transaction,
                                CancellationToken = this.CancellationTokenSource.Token
                            };

                            int res = await dbInterpreter.ExecuteNonQueryAsync(dbConnection, commandInfo);

                            affectedRows += (res == -1 ? 0 : res);
                        }

                        result.Result = affectedRows;

                        if (!dbInterpreter.HasError && !this.cancelRequested)
                        {
                            this.transaction.Commit();
                        }
                    }

                    this.isBusy = false;
                }
            }
            catch (Exception ex)
            {
                this.Rollback(ex);

                result.ResultType = QueryResultType.Text;
                result.HasError = true;
                result.Result = ex.Message;

                this.HandleError(ex);
            }

            return result;
        }

        public async Task Run(DbInterpreter dbInterpreter, IEnumerable<Script> scripts)
        {
            using (DbConnection dbConnection = dbInterpreter.CreateConnection())
            {
                await dbConnection.OpenAsync();

                DbTransaction transaction = await dbConnection.BeginTransactionAsync();

                Func<Script, bool> isValidScript = (s) =>
                {
                    return !(s is NewLineSript || s is SpliterScript || string.IsNullOrEmpty(s.Content) || s.Content == dbInterpreter.ScriptsDelimiter);
                };

                int count = scripts.Where(item => isValidScript(item)).Count();
                int i = 0;

                foreach (Script s in scripts)
                {
                    if (!isValidScript(s))
                    {
                        continue;
                    }

                    string sql = s.Content?.Trim();

                    if (!string.IsNullOrEmpty(sql) && sql != dbInterpreter.ScriptsDelimiter)
                    {
                        i++;

                        if (dbInterpreter.ScriptsDelimiter.Length == 1 && sql.EndsWith(dbInterpreter.ScriptsDelimiter))
                        {
                            sql = sql.TrimEnd(dbInterpreter.ScriptsDelimiter.ToArray());
                        }

                        if (!dbInterpreter.HasError)
                        {
                            CommandInfo commandInfo = new CommandInfo()
                            {
                                CommandType = CommandType.Text,
                                CommandText = sql,
                                Transaction = transaction,
                                CancellationToken = this.CancellationTokenSource.Token
                            };

                            await dbInterpreter.ExecuteNonQueryAsync(dbConnection, commandInfo);
                        }
                    }
                }

                transaction.Commit();
            }
        }

        private void HandleError(Exception ex)
        {
            this.isBusy = false;

            string errMsg = ExceptionHelper.GetExceptionDetails(ex);
            this.Feedback(this, errMsg, FeedbackInfoType.Error, true, true);
        }

        public void Cancle()
        {
            this.cancelRequested = true;

            this.Rollback();

            if (this.CancellationTokenSource != null)
            {
                this.CancellationTokenSource.Cancel();
            }
        }

        private void Rollback(Exception ex = null)
        {
            if (this.transaction != null && this.transaction.Connection != null && this.transaction.Connection.State == ConnectionState.Open)
            {
                try
                {
                    this.cancelRequested = true;

                    bool hasRollbacked = false;

                    if (ex != null && ex is DbCommandException dbe)
                    {
                        hasRollbacked = dbe.HasRollbackedTransaction;
                    }

                    if (!hasRollbacked)
                    {
                        this.transaction.Rollback();
                    }
                }
                catch (Exception e)
                {
                    //throw;
                }
            }
        }

        public void Feedback(object owner, string content, FeedbackInfoType infoType = FeedbackInfoType.Info, bool enableLog = true, bool suppressError = false)
        {
            FeedbackInfo info = new FeedbackInfo() { InfoType = infoType, Message = StringHelper.ToSingleEmptyLine(content), Owner = owner };

            FeedbackHelper.Feedback(suppressError ? null : this.observer, info, enableLog);

            if (this.OnFeedback != null)
            {
                this.OnFeedback(info);
            }
        }
    }
}
