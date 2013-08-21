namespace TheySay.Excel
{
    partial class UserSettingsDlg
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSettingsDlg));
            this.btnSave = new System.Windows.Forms.Button();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword1 = new System.Windows.Forms.Label();
            this.grpCredentials = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.grpAPI = new System.Windows.Forms.GroupBox();
            this.txtLogEntries = new System.Windows.Forms.TextBox();
            this.lblLogEntries = new System.Windows.Forms.Label();
            this.txtAPIEndpoint = new System.Windows.Forms.TextBox();
            this.lblAPIRoot = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpCredentials.SuspendLayout();
            this.grpAPI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(260, 227);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(6, 28);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "&Username";
            // 
            // lblPassword1
            // 
            this.lblPassword1.AutoSize = true;
            this.lblPassword1.Location = new System.Drawing.Point(6, 59);
            this.lblPassword1.Name = "lblPassword1";
            this.lblPassword1.Size = new System.Drawing.Size(53, 13);
            this.lblPassword1.TabIndex = 2;
            this.lblPassword1.Text = "&Password";
            // 
            // grpCredentials
            // 
            this.grpCredentials.Controls.Add(this.txtPassword);
            this.grpCredentials.Controls.Add(this.txtUsername);
            this.grpCredentials.Controls.Add(this.lblUsername);
            this.grpCredentials.Controls.Add(this.lblPassword1);
            this.grpCredentials.Location = new System.Drawing.Point(12, 12);
            this.grpCredentials.Name = "grpCredentials";
            this.grpCredentials.Size = new System.Drawing.Size(323, 92);
            this.grpCredentials.TabIndex = 4;
            this.grpCredentials.TabStop = false;
            this.grpCredentials.Text = "Credentials";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(83, 56);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(207, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Validated += new System.EventHandler(this.ControlValidated);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(83, 26);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(207, 20);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Validated += new System.EventHandler(this.ControlValidated);
            // 
            // grpAPI
            // 
            this.grpAPI.Controls.Add(this.txtLogEntries);
            this.grpAPI.Controls.Add(this.lblLogEntries);
            this.grpAPI.Controls.Add(this.txtAPIEndpoint);
            this.grpAPI.Controls.Add(this.lblAPIRoot);
            this.grpAPI.Location = new System.Drawing.Point(12, 110);
            this.grpAPI.Name = "grpAPI";
            this.grpAPI.Size = new System.Drawing.Size(323, 100);
            this.grpAPI.TabIndex = 5;
            this.grpAPI.TabStop = false;
            this.grpAPI.Text = "API Settings";
            // 
            // txtLogEntries
            // 
            this.txtLogEntries.Location = new System.Drawing.Point(83, 55);
            this.txtLogEntries.Name = "txtLogEntries";
            this.txtLogEntries.Size = new System.Drawing.Size(207, 20);
            this.txtLogEntries.TabIndex = 3;
            this.txtLogEntries.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNumber_Validating);
            this.txtLogEntries.Validated += new System.EventHandler(this.ControlValidated);
            // 
            // lblLogEntries
            // 
            this.lblLogEntries.AutoSize = true;
            this.lblLogEntries.Location = new System.Drawing.Point(6, 58);
            this.lblLogEntries.Name = "lblLogEntries";
            this.lblLogEntries.Size = new System.Drawing.Size(60, 13);
            this.lblLogEntries.TabIndex = 2;
            this.lblLogEntries.Text = "&Log Entries";
            // 
            // txtAPIEndpoint
            // 
            this.txtAPIEndpoint.Location = new System.Drawing.Point(83, 24);
            this.txtAPIEndpoint.Name = "txtAPIEndpoint";
            this.txtAPIEndpoint.Size = new System.Drawing.Size(207, 20);
            this.txtAPIEndpoint.TabIndex = 1;
            this.txtAPIEndpoint.Validating += new System.ComponentModel.CancelEventHandler(this.EndpointValidating);
            this.txtAPIEndpoint.Validated += new System.EventHandler(this.ControlValidated);
            // 
            // lblAPIRoot
            // 
            this.lblAPIRoot.AutoSize = true;
            this.lblAPIRoot.Location = new System.Drawing.Point(6, 27);
            this.lblAPIRoot.Name = "lblAPIRoot";
            this.lblAPIRoot.Size = new System.Drawing.Size(56, 13);
            this.lblAPIRoot.TabIndex = 0;
            this.lblAPIRoot.Text = "&Base URL";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // UserSettingsDlg
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 262);
            this.Controls.Add(this.grpAPI);
            this.Controls.Add(this.grpCredentials);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserSettingsDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Settings";
            this.grpCredentials.ResumeLayout(false);
            this.grpCredentials.PerformLayout();
            this.grpAPI.ResumeLayout(false);
            this.grpAPI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword1;
        private System.Windows.Forms.GroupBox grpCredentials;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.GroupBox grpAPI;
        private System.Windows.Forms.Label lblAPIRoot;
        private System.Windows.Forms.TextBox txtAPIEndpoint;
        private System.Windows.Forms.TextBox txtLogEntries;
        private System.Windows.Forms.Label lblLogEntries;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}