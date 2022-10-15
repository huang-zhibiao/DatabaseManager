﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseManager.Helper;
using DatabaseManager.Profile;
using DatabaseInterpreter.Utility;
using DatabaseManager.Model;
using DatabaseInterpreter.Core;
using DatabaseManager.Core;
using DatabaseInterpreter.Model;
using DatabaseManager.Data;

namespace DatabaseManager.Controls
{
    public partial class UC_DbObjectsNavigator : UserControl
    {
        public ShowDbObjectContentHandler OnShowContent;
        public FeedbackHandler OnFeedback;

        public DatabaseType DatabaseType
        {
            get
            {
                return ManagerUtil.GetDatabaseType(this.cboDbType.Text);
            }
        }

        public UC_DbObjectsNavigator()
        {
            InitializeComponent();
        }

        private void UC_DbObjectsNavigator_Load(object sender, EventArgs e)
        {
            this.InitControls();

            this.tvDbObjects.OnFeedback += this.Feedback;
        }

        private void InitControls()
        {
            this.tvDbObjects.OnShowContent += this.ShowContent;
            this.LoadDbTypes();
        }

        private void ShowContent(DatabaseObjectDisplayInfo content)
        {
            if (this.OnShowContent != null)
            {
                this.OnShowContent(content);
            }
        }

        private void Feedback(FeedbackInfo info)
        {
            if (this.OnFeedback != null)
            {
                this.OnFeedback(info);
            }
        }

        public void LoadDbTypes()
        {
            var databaseTypes = DbInterpreterHelper.GetDisplayDatabaseTypes();
            foreach (var value in databaseTypes)
            {
                this.cboDbType.Items.Add(value.ToString());
            }

            if (this.cboDbType.Items.Count > 0)
            {
                this.cboDbType.Text = SettingManager.Setting.PreferredDatabase.ToString();

                if (string.IsNullOrEmpty(this.cboDbType.Text))
                {
                    this.cboDbType.SelectedIndex = 0;
                }
            }

            this.btnConnect.Focus();
        }

        private void cboDbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool hasValue = this.cboDbType.SelectedIndex >= 0;
            this.btnAddAccount.Enabled = hasValue;

            if (hasValue)
            {
                this.LoadAccounts();
            }
        }

        private void LoadAccounts(Guid? defaultValue = default(Guid?))
        {
            string type = this.cboDbType.Text;

            var profiles = AccountProfileManager.GetProfiles(type).OrderBy(item => item.Description);

            this.cboAccount.DataSource = profiles.ToList();
            this.cboAccount.DisplayMember = nameof(AccountProfileInfo.Description);
            this.cboAccount.ValueMember = nameof(AccountProfileInfo.Id);

            List<Guid> ids = profiles.Select(item => item.Id).ToList();

            if (!defaultValue.HasValue)
            {
                if (profiles.Count() > 0)
                {
                    this.cboAccount.SelectedIndex = 0;
                }
            }
            else
            {
                if (ids.Contains(defaultValue.Value))
                {
                    this.cboAccount.Text = profiles.FirstOrDefault(item => item.Id == defaultValue)?.Description;
                }
            }

            btnConnect.Enabled = this.cboAccount.Items.Count > 0;
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string databaseType = this.cboDbType.Text;

            if (string.IsNullOrEmpty(databaseType))
            {
                MessageBox.Show("Please select a database type first.");
            }
            else
            {
                DatabaseType dbType = ManagerUtil.GetDatabaseType(databaseType);
                frmAccountInfo frmAccountInfo = new frmAccountInfo(dbType);
                DialogResult result = frmAccountInfo.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.LoadAccounts(frmAccountInfo.AccountProfileId);

                    if (this.cboAccount.SelectedItem != null)
                    {
                        (this.cboAccount.SelectedItem as AccountProfileInfo).Password = frmAccountInfo.AccountProfileInfo.Password;
                    }
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(this.Connect));
        }

        private async void Connect()
        {
            AccountProfileInfo profileInfo = this.cboAccount.SelectedItem as AccountProfileInfo;

            if (!profileInfo.IntegratedSecurity && string.IsNullOrEmpty(profileInfo.Password))
            {                
                var storedInfo = DataStore.GetAccountProfileInfo(profileInfo.Id);

                if (storedInfo != null && !profileInfo.IntegratedSecurity && !string.IsNullOrEmpty(storedInfo.Password))
                {
                    profileInfo.Password = storedInfo.Password;
                }
                else
                {
                    MessageBox.Show("Please specify password for the database.");

                    if (!this.SetConnectionInfo(profileInfo))
                    {
                        return;
                    }
                }               
            }

            this.btnConnect.Enabled = false;

            try
            {
                ConnectionInfo connectionInfo = new ConnectionInfo();
                ObjectHelper.CopyProperties(profileInfo, connectionInfo);               

                await this.tvDbObjects.LoadTree(this.DatabaseType, connectionInfo);

                if(SettingManager.Setting.RememberPasswordDuringSession)
                {
                    DataStore.SetAccountProfileInfo(profileInfo);
                }                
            }
            catch (Exception ex)
            {
                this.tvDbObjects.ClearNodes();

                string message = ExceptionHelper.GetExceptionDetails(ex);

                LogHelper.LogError(message);

                MessageBox.Show("Error:" + message);

                if (!this.SetConnectionInfo(profileInfo))
                {
                    return;
                }
                else
                {
                    this.Connect();
                }
            }

            this.btnConnect.Enabled = true;
        }

        private bool SetConnectionInfo(AccountProfileInfo accountProfileInfo)
        {
            DatabaseType dbType = ManagerUtil.GetDatabaseType(this.cboDbType.Text);

            frmAccountInfo frmAccountInfo = new frmAccountInfo(dbType, true) { AccountProfileInfo = accountProfileInfo };

            DialogResult dialogResult = frmAccountInfo.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                AccountProfileInfo profileInfo = frmAccountInfo.AccountProfileInfo;
                ObjectHelper.CopyProperties(profileInfo, (this.cboAccount.SelectedItem as AccountProfileInfo));
                this.cboAccount.Text = profileInfo.Description;                

                return true;
            }
            else
            {
                this.btnConnect.Enabled = true;
            }

            return false;
        }

        public ConnectionInfo GetCurrentConnectionInfo()
        {
            return this.tvDbObjects.GetCurrentConnectionInfo();
        }

        public DatabaseObjectDisplayInfo GetDisplayInfo()
        {
            DatabaseObjectDisplayInfo info = this.tvDbObjects.GetDisplayInfo();

            info.DatabaseType = this.DatabaseType;

            return info;
        }

        private void UC_DbObjectsNavigator_SizeChanged(object sender, EventArgs e)
        {
            this.cboDbType.Refresh();
            this.cboAccount.Refresh();
        }
    }
}
