using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using TheySay.Rest.Api;

namespace TheySay.Excel
{
    public partial class AccountStatusDlg : Form
    {
        public ApiControlData Data { get; set; }
        public bool ApiStatus { get; set; }
        public ApiThrottle ApiThrottle { get; set; }

        public AccountStatusDlg()
        {
            InitializeComponent();
            lblVersion.Text += Assembly.GetExecutingAssembly().GetName().Version;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Data != null)
            {
                txtNextWindowStart.Text = Data.NextWindowTime.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss");
                txtWindowLimit.Text = Data.WindowLimit.ToString(CultureInfo.CurrentCulture);
                txtWindowRemaining.Text = Data.WindowRemaining.ToString(CultureInfo.CurrentCulture);
                txtIntervalLength.Text = Data.IntervalInSecs.ToString(CultureInfo.CurrentCulture);

                txtDayStart.Text = Data.DayWindowResetTime.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss");
                txtDayLimit.Text = Data.DayLimit.ToString(CultureInfo.CurrentCulture);
                txtDayRemaining.Text = Data.DayRemaining.ToString(CultureInfo.CurrentCulture);
            }
            else
            {
                lblWarning.Visible = true;
            }
            SetApiStatus(ApiStatus);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            var pingResp = await ApiThrottle.Get("/ping");

            SetApiStatus(pingResp.Success);
        }

        private void SetApiStatus(bool success)
        {
            txtAPIStatus.ForeColor = success ? Color.LimeGreen : Color.Red;
            txtAPIStatus.Text = success ? "Active" : "Inactive";
        }
    }
}
