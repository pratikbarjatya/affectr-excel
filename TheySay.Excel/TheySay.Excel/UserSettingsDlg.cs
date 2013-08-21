using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TheySay.Excel
{
    public partial class UserSettingsDlg : Form
    {
        public UserSettingsDlg()
        {
            InitializeComponent();
            PopulateData();
        }

        private void PopulateData()
        {
            txtUsername.Text = Excel.Default.Username;
            txtPassword.Text = Excel.Default.Password;
            txtLogEntries.Text = Excel.Default.LogEntries.ToString(CultureInfo.InvariantCulture);
            txtAPIEndpoint.Text = Excel.Default.ApiEndpoint;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                // save to config
                Excel.Default.Username = txtUsername.Text;
                Excel.Default.Password = txtPassword.Text;
                Excel.Default.LogEntries = int.Parse(txtLogEntries.Text);
                Excel.Default.ApiEndpoint= txtAPIEndpoint.Text;

                Excel.Default.Save();
                Close();
            }
        }
        private void EndpointValidating(object sender, CancelEventArgs e)
        {
            var regex = new Regex(@"^https?://");
            var match = regex.Match(((Control) sender).Text);

            if (!match.Success)
            {
                errorProvider.SetError((Control)sender, @"Value must begin with http[s]://");
                e.Cancel = true;
            }
        }

        private void textBoxNumber_Validating(object sender, CancelEventArgs e)
        {
            bool cancel = false;
            int number;
            if (int.TryParse(txtLogEntries.Text, out number))
            {
                if (number < 1 || number > 100000)
                {
                    //This control has failed validation: number is not in valid range
                    cancel = true;
                    errorProvider.SetError(txtLogEntries, "You must provide a number between 1 and 100000!");
                }
            }
            else
            {
                //This control has failed validation: text box is not a number
                cancel = true;
                errorProvider.SetError(txtLogEntries, "You must provide a valid number!");
            }
            e.Cancel = cancel;
        }

        private void ControlValidated(object sender, EventArgs e)
        {
            //Control has validated, clear any error message.
            errorProvider.SetError((Control)sender, string.Empty);
        } 
    }
}
