namespace TheySay.Excel
{
    partial class Ribbon1 : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon1()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.Analysis = this.Factory.CreateRibbonGroup();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.btnTheySay = this.Factory.CreateRibbonButton();
            this.btnAnalyseEntity = this.Factory.CreateRibbonButton();
            this.btnAnalyseSentence = this.Factory.CreateRibbonButton();
            this.btnAnalyseDocument = this.Factory.CreateRibbonButton();
            this.btnAccountStatus = this.Factory.CreateRibbonButton();
            this.btnActivityLog = this.Factory.CreateRibbonButton();
            this.btnLoginSettings = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.Analysis.SuspendLayout();
            this.group2.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.Groups.Add(this.group1);
            this.tab1.Groups.Add(this.Analysis);
            this.tab1.Groups.Add(this.group2);
            this.tab1.Label = "TheySay";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.btnTheySay);
            this.group1.Label = "Sentiment Analysis";
            this.group1.Name = "group1";
            // 
            // Analysis
            // 
            this.Analysis.Items.Add(this.btnAnalyseEntity);
            this.Analysis.Items.Add(this.btnAnalyseSentence);
            this.Analysis.Items.Add(this.btnAnalyseDocument);
            this.Analysis.Label = "Run Analysis";
            this.Analysis.Name = "Analysis";
            // 
            // group2
            // 
            this.group2.Items.Add(this.btnAccountStatus);
            this.group2.Items.Add(this.btnActivityLog);
            this.group2.Items.Add(this.btnLoginSettings);
            this.group2.Label = "Account Settings";
            this.group2.Name = "group2";
            // 
            // btnTheySay
            // 
            this.btnTheySay.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnTheySay.Image = global::TheySay.Excel.Properties.Resources.TheySay;
            this.btnTheySay.Label = "TheySay";
            this.btnTheySay.Name = "btnTheySay";
            this.btnTheySay.ShowImage = true;
            this.btnTheySay.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnTheySay_Click);
            // 
            // btnAnalyseEntity
            // 
            this.btnAnalyseEntity.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnAnalyseEntity.Label = "Analyse by Entity";
            this.btnAnalyseEntity.Name = "btnAnalyseEntity";
            this.btnAnalyseEntity.OfficeImageId = "ViewsLayoutView";
            this.btnAnalyseEntity.ShowImage = true;
            this.btnAnalyseEntity.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAnalyseEntity_Click);
            // 
            // btnAnalyseSentence
            // 
            this.btnAnalyseSentence.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnAnalyseSentence.Label = "Analyse by Sentence";
            this.btnAnalyseSentence.Name = "btnAnalyseSentence";
            this.btnAnalyseSentence.OfficeImageId = "CreateLabels";
            this.btnAnalyseSentence.ShowImage = true;
            this.btnAnalyseSentence.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAnalyseSentence_Click);
            // 
            // btnAnalyseDocument
            // 
            this.btnAnalyseDocument.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnAnalyseDocument.Label = "Analyse by Document";
            this.btnAnalyseDocument.Name = "btnAnalyseDocument";
            this.btnAnalyseDocument.OfficeImageId = "ReadingViewShowPrintedPage";
            this.btnAnalyseDocument.ShowImage = true;
            this.btnAnalyseDocument.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAnalyseDocument_Click);
            // 
            // btnAccountStatus
            // 
            this.btnAccountStatus.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnAccountStatus.Label = "Account Status";
            this.btnAccountStatus.Name = "btnAccountStatus";
            this.btnAccountStatus.OfficeImageId = "DatabasePermissionsMenu";
            this.btnAccountStatus.ShowImage = true;
            this.btnAccountStatus.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAccountStatus_Click);
            // 
            // btnActivityLog
            // 
            this.btnActivityLog.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnActivityLog.Label = "Activity Log";
            this.btnActivityLog.Name = "btnActivityLog";
            this.btnActivityLog.OfficeImageId = "QueryUpdate";
            this.btnActivityLog.ShowImage = true;
            this.btnActivityLog.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnActivityLog_Click);
            // 
            // btnLoginSettings
            // 
            this.btnLoginSettings.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnLoginSettings.Label = "Login Settings";
            this.btnLoginSettings.Name = "btnLoginSettings";
            this.btnLoginSettings.OfficeImageId = "ControlsGallery";
            this.btnLoginSettings.ShowImage = true;
            this.btnLoginSettings.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLoginSettings_Click);
            // 
            // Ribbon1
            // 
            this.Name = "Ribbon1";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.Analysis.ResumeLayout(false);
            this.Analysis.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Analysis;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAnalyseEntity;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAnalyseSentence;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAnalyseDocument;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnTheySay;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAccountStatus;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnActivityLog;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLoginSettings;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon1 Ribbon1
        {
            get { return this.GetRibbon<Ribbon1>(); }
        }
    }
}
