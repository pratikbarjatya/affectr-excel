namespace TheySay.Excel
{
    partial class AccountStatusDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountStatusDlg));
            this.btnOk = new System.Windows.Forms.Button();
            this.grpWindowStatus = new System.Windows.Forms.GroupBox();
            this.txtIntervalLength = new System.Windows.Forms.TextBox();
            this.lblIntervalLength = new System.Windows.Forms.Label();
            this.txtNextWindowStart = new System.Windows.Forms.TextBox();
            this.txtWindowLimit = new System.Windows.Forms.TextBox();
            this.txtWindowRemaining = new System.Windows.Forms.TextBox();
            this.lblNextWindowStart = new System.Windows.Forms.Label();
            this.lblWindowLimit = new System.Windows.Forms.Label();
            this.lblWindowRemaining = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAPIStatus = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblWarning = new System.Windows.Forms.Label();
            this.grpDayStatus = new System.Windows.Forms.GroupBox();
            this.txtDayStart = new System.Windows.Forms.TextBox();
            this.lblDayStart = new System.Windows.Forms.Label();
            this.txtDayLimit = new System.Windows.Forms.TextBox();
            this.lblDayLimit = new System.Windows.Forms.Label();
            this.txtDayRemaining = new System.Windows.Forms.TextBox();
            this.lblDayRemaining = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.grpWindowStatus.SuspendLayout();
            this.grpDayStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(207, 334);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // grpWindowStatus
            // 
            this.grpWindowStatus.Controls.Add(this.txtIntervalLength);
            this.grpWindowStatus.Controls.Add(this.lblIntervalLength);
            this.grpWindowStatus.Controls.Add(this.txtNextWindowStart);
            this.grpWindowStatus.Controls.Add(this.txtWindowLimit);
            this.grpWindowStatus.Controls.Add(this.txtWindowRemaining);
            this.grpWindowStatus.Controls.Add(this.lblNextWindowStart);
            this.grpWindowStatus.Controls.Add(this.lblWindowLimit);
            this.grpWindowStatus.Controls.Add(this.lblWindowRemaining);
            this.grpWindowStatus.Location = new System.Drawing.Point(12, 46);
            this.grpWindowStatus.Name = "grpWindowStatus";
            this.grpWindowStatus.Size = new System.Drawing.Size(294, 133);
            this.grpWindowStatus.TabIndex = 3;
            this.grpWindowStatus.TabStop = false;
            this.grpWindowStatus.Text = "Rate Limit Statistics";
            // 
            // txtIntervalLength
            // 
            this.txtIntervalLength.Location = new System.Drawing.Point(143, 95);
            this.txtIntervalLength.Name = "txtIntervalLength";
            this.txtIntervalLength.ReadOnly = true;
            this.txtIntervalLength.Size = new System.Drawing.Size(127, 20);
            this.txtIntervalLength.TabIndex = 7;
            // 
            // lblIntervalLength
            // 
            this.lblIntervalLength.AutoSize = true;
            this.lblIntervalLength.Location = new System.Drawing.Point(23, 98);
            this.lblIntervalLength.Name = "lblIntervalLength";
            this.lblIntervalLength.Size = new System.Drawing.Size(104, 13);
            this.lblIntervalLength.TabIndex = 6;
            this.lblIntervalLength.Text = "Interval Length (sec)";
            // 
            // txtNextWindowStart
            // 
            this.txtNextWindowStart.Location = new System.Drawing.Point(143, 69);
            this.txtNextWindowStart.Name = "txtNextWindowStart";
            this.txtNextWindowStart.ReadOnly = true;
            this.txtNextWindowStart.Size = new System.Drawing.Size(127, 20);
            this.txtNextWindowStart.TabIndex = 5;
            // 
            // txtWindowLimit
            // 
            this.txtWindowLimit.Location = new System.Drawing.Point(143, 43);
            this.txtWindowLimit.Name = "txtWindowLimit";
            this.txtWindowLimit.ReadOnly = true;
            this.txtWindowLimit.Size = new System.Drawing.Size(127, 20);
            this.txtWindowLimit.TabIndex = 4;
            // 
            // txtWindowRemaining
            // 
            this.txtWindowRemaining.Location = new System.Drawing.Point(143, 17);
            this.txtWindowRemaining.Name = "txtWindowRemaining";
            this.txtWindowRemaining.ReadOnly = true;
            this.txtWindowRemaining.Size = new System.Drawing.Size(127, 20);
            this.txtWindowRemaining.TabIndex = 3;
            // 
            // lblNextWindowStart
            // 
            this.lblNextWindowStart.AutoSize = true;
            this.lblNextWindowStart.Location = new System.Drawing.Point(23, 72);
            this.lblNextWindowStart.Name = "lblNextWindowStart";
            this.lblNextWindowStart.Size = new System.Drawing.Size(96, 13);
            this.lblNextWindowStart.TabIndex = 2;
            this.lblNextWindowStart.Text = "Next Window Start";
            // 
            // lblWindowLimit
            // 
            this.lblWindowLimit.AutoSize = true;
            this.lblWindowLimit.Location = new System.Drawing.Point(23, 46);
            this.lblWindowLimit.Name = "lblWindowLimit";
            this.lblWindowLimit.Size = new System.Drawing.Size(48, 13);
            this.lblWindowLimit.TabIndex = 1;
            this.lblWindowLimit.Text = "Call Limit";
            // 
            // lblWindowRemaining
            // 
            this.lblWindowRemaining.AutoSize = true;
            this.lblWindowRemaining.Location = new System.Drawing.Point(24, 20);
            this.lblWindowRemaining.Name = "lblWindowRemaining";
            this.lblWindowRemaining.Size = new System.Drawing.Size(82, 13);
            this.lblWindowRemaining.TabIndex = 0;
            this.lblWindowRemaining.Text = "Remaining Calls";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "API Status";
            // 
            // txtAPIStatus
            // 
            this.txtAPIStatus.BackColor = System.Drawing.SystemColors.Control;
            this.txtAPIStatus.Location = new System.Drawing.Point(155, 10);
            this.txtAPIStatus.Name = "txtAPIStatus";
            this.txtAPIStatus.ReadOnly = true;
            this.txtAPIStatus.Size = new System.Drawing.Size(70, 20);
            this.txtAPIStatus.TabIndex = 5;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(231, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.ForeColor = System.Drawing.Color.Red;
            this.lblWarning.Location = new System.Drawing.Point(21, 304);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(279, 13);
            this.lblWarning.TabIndex = 7;
            this.lblWarning.Text = "Statistics are not availble until an API call has been made.";
            this.lblWarning.Visible = false;
            // 
            // grpDayStatus
            // 
            this.grpDayStatus.Controls.Add(this.txtDayStart);
            this.grpDayStatus.Controls.Add(this.lblDayStart);
            this.grpDayStatus.Controls.Add(this.txtDayLimit);
            this.grpDayStatus.Controls.Add(this.lblDayLimit);
            this.grpDayStatus.Controls.Add(this.txtDayRemaining);
            this.grpDayStatus.Controls.Add(this.lblDayRemaining);
            this.grpDayStatus.Location = new System.Drawing.Point(13, 185);
            this.grpDayStatus.Name = "grpDayStatus";
            this.grpDayStatus.Size = new System.Drawing.Size(293, 103);
            this.grpDayStatus.TabIndex = 8;
            this.grpDayStatus.TabStop = false;
            this.grpDayStatus.Text = "Day Statistics";
            // 
            // txtDayStart
            // 
            this.txtDayStart.Location = new System.Drawing.Point(142, 71);
            this.txtDayStart.Name = "txtDayStart";
            this.txtDayStart.ReadOnly = true;
            this.txtDayStart.Size = new System.Drawing.Size(127, 20);
            this.txtDayStart.TabIndex = 9;
            // 
            // lblDayStart
            // 
            this.lblDayStart.AutoSize = true;
            this.lblDayStart.Location = new System.Drawing.Point(22, 74);
            this.lblDayStart.Name = "lblDayStart";
            this.lblDayStart.Size = new System.Drawing.Size(76, 13);
            this.lblDayStart.TabIndex = 8;
            this.lblDayStart.Text = "Next Day Start";
            // 
            // txtDayLimit
            // 
            this.txtDayLimit.Location = new System.Drawing.Point(142, 45);
            this.txtDayLimit.Name = "txtDayLimit";
            this.txtDayLimit.ReadOnly = true;
            this.txtDayLimit.Size = new System.Drawing.Size(127, 20);
            this.txtDayLimit.TabIndex = 7;
            // 
            // lblDayLimit
            // 
            this.lblDayLimit.AutoSize = true;
            this.lblDayLimit.Location = new System.Drawing.Point(22, 48);
            this.lblDayLimit.Name = "lblDayLimit";
            this.lblDayLimit.Size = new System.Drawing.Size(48, 13);
            this.lblDayLimit.TabIndex = 6;
            this.lblDayLimit.Text = "Call Limit";
            // 
            // txtDayRemaining
            // 
            this.txtDayRemaining.Location = new System.Drawing.Point(142, 19);
            this.txtDayRemaining.Name = "txtDayRemaining";
            this.txtDayRemaining.ReadOnly = true;
            this.txtDayRemaining.Size = new System.Drawing.Size(127, 20);
            this.txtDayRemaining.TabIndex = 5;
            // 
            // lblDayRemaining
            // 
            this.lblDayRemaining.AutoSize = true;
            this.lblDayRemaining.Location = new System.Drawing.Point(23, 22);
            this.lblDayRemaining.Name = "lblDayRemaining";
            this.lblDayRemaining.Size = new System.Drawing.Size(82, 13);
            this.lblDayRemaining.TabIndex = 4;
            this.lblDayRemaining.Text = "Remaining Calls";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(21, 339);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(48, 13);
            this.lblVersion.TabIndex = 9;
            this.lblVersion.Text = "Version: ";
            // 
            // AccountStatusDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 369);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.grpDayStatus);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.txtAPIStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpWindowStatus);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountStatusDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Account Status";
            this.grpWindowStatus.ResumeLayout(false);
            this.grpWindowStatus.PerformLayout();
            this.grpDayStatus.ResumeLayout(false);
            this.grpDayStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox grpWindowStatus;
        private System.Windows.Forms.Label lblNextWindowStart;
        private System.Windows.Forms.Label lblWindowLimit;
        private System.Windows.Forms.Label lblWindowRemaining;
        private System.Windows.Forms.TextBox txtNextWindowStart;
        private System.Windows.Forms.TextBox txtWindowLimit;
        private System.Windows.Forms.TextBox txtWindowRemaining;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAPIStatus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.TextBox txtIntervalLength;
        private System.Windows.Forms.Label lblIntervalLength;
        private System.Windows.Forms.GroupBox grpDayStatus;
        private System.Windows.Forms.TextBox txtDayStart;
        private System.Windows.Forms.Label lblDayStart;
        private System.Windows.Forms.TextBox txtDayLimit;
        private System.Windows.Forms.Label lblDayLimit;
        private System.Windows.Forms.TextBox txtDayRemaining;
        private System.Windows.Forms.Label lblDayRemaining;
        private System.Windows.Forms.Label lblVersion;
    }
}