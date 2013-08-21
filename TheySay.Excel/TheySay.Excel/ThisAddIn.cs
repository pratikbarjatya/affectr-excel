using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools;

namespace TheySay.Excel
{
    public partial class ThisAddIn
    {
        public CustomTaskPane LogPane { get; private set; }
        public LogPane Pane { get; private set; }
        public LogBuffer LogBuffer { get; private set; }

        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            Pane = new LogPane();
            LogBuffer = new LogBuffer(Excel.Default.LogEntries);
        }

        public bool LogPaneVisible()
        {
            var visible = false;
            var currWindow = Application.Windows[1];

            foreach (var pane in CustomTaskPanes)
            {
                if (((Window)pane.Window).Hwnd == currWindow.Hwnd)
                {
                    visible = true;
                    LogPane = pane;
                    Pane = LogPane.Control as LogPane;
                    break;
                }
            }
            Trace.WriteLine("LogPaneVisible: " + visible);
            return visible;
        }

        public void CreateLogPane()
        {
            try
            {
                var removeItems = new List<CustomTaskPane>();
                // remove non-visible panes
                foreach (var pane in CustomTaskPanes)
                {
                    try
                    {
                        if (!pane.Visible)
                            removeItems.Add(pane);
                    }
                    catch (Exception)
                    {
                        removeItems.Add(pane);
                    }
                }
                foreach (var customTaskPane in removeItems)
                {
                    CustomTaskPanes.Remove(customTaskPane);
                }
                var currWindow = Application.Windows[1];

                var visible = false;
                foreach (var pane in CustomTaskPanes)
                {
                    if (((Window)pane.Window).Hwnd == currWindow.Hwnd)
                    {
                        visible = true;
                        LogPane = pane;
                        Pane = LogPane.Control as LogPane;
                        break;
                    }
                }

                if (!visible)
                {
                    Pane = new LogPane();

                    LogPane = CustomTaskPanes.Add(Pane, "TheySay Log", currWindow);
                }
                LogPane.Visible = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
