using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace TheySay.Excel
{
    public partial class LogPane : UserControl
    {
        public LogPane()
        {
            InitializeComponent();
        }

        public void AddText(string text)
        {
            txtLog.Text += text + Environment.NewLine;
        }

        public void RefreshLog()
        {
            try
            {
                var sb = new StringBuilder();
                foreach (var entry in Globals.ThisAddIn.LogBuffer.Entries())
                {
                    sb.AppendLine(entry.ToString());
                }
                if (!txtLog.InvokeRequired)
                {
                    txtLog.Clear();
                    txtLog.Text = sb.ToString();
                }
                else
                {
                    txtLog.Invoke((MethodInvoker)(() =>
                    {
                        txtLog.Clear(); 
                        txtLog.Text = sb.ToString();
                    }));
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Globals.ThisAddIn.LogBuffer.Clear();
            txtLog.Clear();
        }
    }
}
