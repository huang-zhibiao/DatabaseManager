﻿namespace DatabaseManager
{
    partial class frmDbConnectionManage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvDbConnection = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Control = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Word = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.UseCodeTemplate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDbType = new System.Windows.Forms.ComboBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.panelOperation = new System.Windows.Forms.Panel();
            this.panelDbType = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDbConnection)).BeginInit();
            this.panelOperation.SuspendLayout();
            this.panelDbType.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(624, 358);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 33);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvDbConnection
            // 
            this.dgvDbConnection.AllowUserToAddRows = false;
            this.dgvDbConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDbConnection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDbConnection.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Control,
            this.Word,
            this.UseCodeTemplate,
            this.Port});
            this.dgvDbConnection.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDbConnection.Location = new System.Drawing.Point(5, 45);
            this.dgvDbConnection.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDbConnection.Name = "dgvDbConnection";
            this.dgvDbConnection.ReadOnly = true;
            this.dgvDbConnection.RowHeadersVisible = false;
            this.dgvDbConnection.RowHeadersWidth = 20;
            this.dgvDbConnection.RowTemplate.Height = 23;
            this.dgvDbConnection.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDbConnection.Size = new System.Drawing.Size(716, 302);
            this.dgvDbConnection.TabIndex = 20;
            this.dgvDbConnection.DoubleClick += new System.EventHandler(this.dgvDbConnection_DoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // Control
            // 
            this.Control.DataPropertyName = "Server";
            this.Control.HeaderText = "Server";
            this.Control.Name = "Control";
            this.Control.ReadOnly = true;
            this.Control.Width = 250;
            // 
            // Word
            // 
            this.Word.DataPropertyName = "IntegratedSecurity";
            this.Word.HeaderText = "Integrated Security";
            this.Word.Name = "Word";
            this.Word.ReadOnly = true;
            this.Word.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Word.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Word.Width = 150;
            // 
            // UseCodeTemplate
            // 
            this.UseCodeTemplate.DataPropertyName = "UserId";
            this.UseCodeTemplate.HeaderText = "User Name";
            this.UseCodeTemplate.Name = "UseCodeTemplate";
            this.UseCodeTemplate.ReadOnly = true;
            this.UseCodeTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UseCodeTemplate.Width = 150;
            // 
            // Port
            // 
            this.Port.HeaderText = "Port";
            this.Port.Name = "Port";
            this.Port.ReadOnly = true;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(340, 4);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 33);
            this.btnClear.TabIndex = 23;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(230, 4);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(88, 33);
            this.btnDelete.TabIndex = 22;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(10, 4);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 33);
            this.btnAdd.TabIndex = 21;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Database Type:";
            // 
            // cboDbType
            // 
            this.cboDbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDbType.FormattingEnabled = true;
            this.cboDbType.Location = new System.Drawing.Point(119, 3);
            this.cboDbType.Margin = new System.Windows.Forms.Padding(4);
            this.cboDbType.Name = "cboDbType";
            this.cboDbType.Size = new System.Drawing.Size(106, 25);
            this.cboDbType.TabIndex = 37;
            this.cboDbType.SelectedIndexChanged += new System.EventHandler(this.cboDbType_SelectedIndexChanged);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Location = new System.Drawing.Point(120, 4);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(88, 33);
            this.btnEdit.TabIndex = 38;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(530, 358);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(88, 33);
            this.btnSelect.TabIndex = 39;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Visible = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // panelOperation
            // 
            this.panelOperation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelOperation.Controls.Add(this.btnEdit);
            this.panelOperation.Controls.Add(this.btnAdd);
            this.panelOperation.Controls.Add(this.btnDelete);
            this.panelOperation.Controls.Add(this.btnClear);
            this.panelOperation.Location = new System.Drawing.Point(5, 354);
            this.panelOperation.Margin = new System.Windows.Forms.Padding(4);
            this.panelOperation.Name = "panelOperation";
            this.panelOperation.Size = new System.Drawing.Size(446, 44);
            this.panelOperation.TabIndex = 40;
            // 
            // panelDbType
            // 
            this.panelDbType.Controls.Add(this.label1);
            this.panelDbType.Controls.Add(this.cboDbType);
            this.panelDbType.Location = new System.Drawing.Point(5, 3);
            this.panelDbType.Margin = new System.Windows.Forms.Padding(4);
            this.panelDbType.Name = "panelDbType";
            this.panelDbType.Size = new System.Drawing.Size(422, 34);
            this.panelDbType.TabIndex = 41;
            // 
            // frmDbConnectionManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 408);
            this.Controls.Add(this.panelDbType);
            this.Controls.Add(this.panelOperation);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.dgvDbConnection);
            this.Controls.Add(this.btnCancel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmDbConnectionManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Connection Manage";
            this.Load += new System.EventHandler(this.frmDbConnectionManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDbConnection)).EndInit();
            this.panelOperation.ResumeLayout(false);
            this.panelDbType.ResumeLayout(false);
            this.panelDbType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvDbConnection;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDbType;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Panel panelOperation;
        private System.Windows.Forms.Panel panelDbType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Control;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Word;
        private System.Windows.Forms.DataGridViewTextBoxColumn UseCodeTemplate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Port;
    }
}