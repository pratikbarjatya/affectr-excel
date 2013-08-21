using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Excel;
using TheySay.Rest.Api;
using System.Threading.Tasks;
using System.Threading;
using Action = System.Action;

namespace TheySay.Excel
{
    public partial class Ribbon1
    {
        private ApiThrottle _throttleApi;
        private string _documentSheetName;
        private string _entitySheetName;
        private string _sentenceSheetName;

        private int _sentenceIndex;
        private int _entityIndex;

        private const string SelectRange = "Please select a cell or range of cells.";

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            _throttleApi = new ApiThrottle(Excel.Default.Username, Excel.Default.Password, Excel.Default.ApiEndpoint, Excel.Default.WarnThreshold);
        }

        private async void btnAnalyseEntity_Click(object sender, RibbonControlEventArgs e)
        {
            btnAnalyseEntity.Enabled = false;

            var range = GetSelection();
            if (range == null)
            {
                MessageBox.Show(SelectRange);
                btnAnalyseEntity.Enabled = true;
                return;
            }

            // Get all the cell text to analyse
            var cells = GetRangeText(range);

            try
            {
                await _throttleApi.AnalyseEntity(cells, ProcessEntityResults, WarnTimeThreshold);
            }
            catch (ApplicationException ex)
            {
                ShowUserError(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }

            _entitySheetName = string.Empty;
            btnAnalyseEntity.Enabled = true;
        }

        private async void btnAnalyseSentence_Click(object sender, RibbonControlEventArgs e)
        {
            btnAnalyseSentence.Enabled = false;

            var range = GetSelection();
            if (range == null)
            {
                MessageBox.Show(SelectRange);
                btnAnalyseSentence.Enabled = true;
                return;
            }

            // Get all the cell text to analyse
            var cells = GetRangeText(range);

            try
            {
                await _throttleApi.AnalyseSentence(cells, ProcessSentenceResults, WarnTimeThreshold);
            }
            catch (ApplicationException ex)
            {
                ShowUserError(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }

            _entitySheetName = string.Empty;
            btnAnalyseSentence.Enabled = true;
        }

        private int ProcessSentenceResults(List<Task<ResponseWrapper<SentimentSentenceResponse[]>>> data, List<AnalysisCell> cells, int index)
        {
            try
            {
                LockUi();

                Trace.WriteLine("ProcessSentenceResults index: " + index);
                Worksheet newWorksheet = index == 0 ? CreateSheet("Sentence Sentiment") : FindSheet(_sentenceSheetName);

                if (index == 0)
                {
                    _sentenceSheetName = newWorksheet.Name;
                    var entityHeadings = new[]
                                             {
                                                 "Input Cell",
                                                 "Sentence Index",
                                                 "Text",
                                                 "Start",
                                                 "End",
                                                 "Label",
                                                 "Positive",
                                                 "Negative",
                                                 "Neutral",
                                                 "Confidence"
                                             };
                    WriteHeadings(newWorksheet, entityHeadings);
                    index++;
                    _sentenceIndex = index;
                }
                else
                {
                    index = 1; // because of heading 
                }


                foreach (var rec in data)
                {
                    var logentry = new LogEntry {ApiCall = "Sentence Analysis", RequestData = rec.Result.Input};

                    var listData = new object[rec.Result.Response == null ? 1 : rec.Result.Response.Length, 12];
                    var startrow = _sentenceIndex + 1;
                    var rowIndex = 0;

                    if (rec.Result.Success)
                    {
                        foreach (var rsp in rec.Result.Response)
                        {
                            int col = 0;

                            listData[rowIndex, col++] = rec.Result.Input.Address;
                            listData[rowIndex, col++] = rsp.sentenceIndex;
                            listData[rowIndex, col++] = rsp.text;
                            listData[rowIndex, col++] = rsp.start;
                            listData[rowIndex, col++] = rsp.end;
                            listData[rowIndex, col++] = rsp.sentiment.label;
                            listData[rowIndex, col++] = rsp.sentiment.positive;
                            listData[rowIndex, col++] = rsp.sentiment.negative;
                            listData[rowIndex, col++] = rsp.sentiment.neutral;
                            listData[rowIndex, col++] = rsp.sentiment.confidence;
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col] = "";

                            rowIndex++;
                            index++;
                            _sentenceIndex++;
                        }
                    }
                    else
                    {
                        if (!rec.Result.ErrorMessage.Contains("429"))
                        {
                            logentry.ErrorMessage = rec.Result.ErrorMessage;

                            int col = 0;
                            listData[rowIndex, col++] = rec.Result.Input.Address;
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = rec.Result.Input.Text;
                            listData[rowIndex, col] = rec.Result.ErrorMessage;

                            index++;
                            rowIndex++;
                            _sentenceIndex++;
                        }
                    }
                    WriteRange(startrow, startrow + rowIndex, 12, newWorksheet, listData);

                    Globals.ThisAddIn.LogBuffer.AddEntry(logentry);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                Globals.ThisAddIn.LogBuffer.AddEntry(new LogEntry { ApiCall = "Sentence Analysis", ErrorMessage = ex.Message });
            }
            finally
            {
                // unlock
                ReEnable();                
            }

            return index;
        }

        private static void WriteHeadings(Worksheet newWorksheet, string[] entityHeadings)
        {
            var startCell = (Range) newWorksheet.Cells[1, 1];
            var endCell = (Range) newWorksheet.Cells[1, entityHeadings.Length];
            var writeRange = newWorksheet.Range[startCell, endCell];

            // add headings
            writeRange.Value2 = entityHeadings;
            writeRange.Font.Bold = true;
            writeRange.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSkyBlue);
            writeRange.Columns.AutoFit();
        }

        private static void WriteRange(int startrow, int endrow, int toCol, Worksheet worksheet, object[,] data)
        {
            Trace.WriteLine(string.Format("startrow: {0} endrow: {1}", startrow, endrow));
            var startCell = (Range)worksheet.Cells[startrow, 1];
            var endCell = (Range)worksheet.Cells[endrow - 1, toCol];
            var writeRange = worksheet.Range[startCell, endCell];

            ExcelRetryCall(() => { writeRange.Value2 = data; });
        }

        private async void btnAnalyseDocument_Click(object sender, RibbonControlEventArgs e)
        {
            btnAnalyseDocument.Enabled = false;

            var range = GetSelection();
            if (range == null)
            {
                MessageBox.Show(SelectRange);
                btnAnalyseDocument.Enabled = true;
                return;
            }

            // Get all the cell text to analyse
            var cells = GetRangeText(range);

            try
            {
                await _throttleApi.AnalyseDocument(cells, ProcessDocumentResults, WarnTimeThreshold);
            }
            catch (ApplicationException ex)
            {
                ShowUserError(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }

            _documentSheetName = string.Empty;
            btnAnalyseDocument.Enabled = true;
        }

        public bool WarnTimeThreshold(int mins)
        {
            return
                MessageBox.Show("The next threshold is in " + mins + " minutes. Do you want to continue and wait?",
                                "Rate Limited", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private static void LockUi()
        {
            try
            {
                ExcelRetryCall(() =>
                {
                    Globals.ThisAddIn.Application.Interactive = false;
                    Globals.ThisAddIn.Application.ScreenUpdating = false;
                });

            }
            catch (Exception ex)
            {
                Globals.ThisAddIn.LogBuffer.AddEntry(new LogEntry { ApiCall = "Attempt to Lock user interface", ErrorMessage = ex.Message });
            }
        }

        private static void UnlockUi()
        {
            ExcelRetryCall(() =>
                               {
                                   Globals.ThisAddIn.Application.ScreenUpdating = true;
                                   Globals.ThisAddIn.Application.Interactive = true;
                               });
        }

        private static void ExcelRetryCall(Action call)
        {
            // can get a com exception 0x800AC472 if the user starts clicking around or doing stuff when we are also trying to update excel.
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    call();
                    break;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("0x800AC472"))
                        Thread.Sleep(100);
                    else if (ex.Message.Contains("0x8001010A") || ex.Message.Contains("0x800A03EC"))
                    {
                        MessageBox.Show("Please finish your current action so that the plugin can update the worksheet.");
                        Thread.Sleep(3000);
                    }
                    else
                        throw;
                }
            }
        }

        private void ReEnable()
        {
            UnlockUi();

            if (Globals.ThisAddIn.LogPaneVisible())
            {
                Globals.ThisAddIn.Pane.RefreshLog();
            }
        }

        // Helpers

        private static List<AnalysisCell> GetRangeText(Range range)
        {
            return (range.Cells.Cast<Range>()
                         .Where(item => !string.IsNullOrWhiteSpace(item.Value2))
                         .Select(item => new AnalysisCell
                                             {
                                                 Address = item.Parent.name + "!" + item.AddressLocal[false, false],
                                                 Text = item.Value2.ToString()
                                             })).ToList();
        }

        /// <summary>
        /// Create a new worksheet with the given name prefix and a numeric index postfix to make it unique.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Worksheet CreateSheet(string name)
        {
            var found = true;
            var i = 1;
            Worksheet newWorksheet;
            do
            {
                var searchName = name + " " + i++;
                newWorksheet = Globals.ThisAddIn.Application.Worksheets.SheetExists(searchName);
                if (newWorksheet == null)
                {
                    // sheet names are limited to 31 chars
                    newWorksheet = Globals.ThisAddIn.Application.Worksheets.Add(After: Globals.ThisAddIn.Application.ActiveSheet);
                    newWorksheet.Name = searchName;
                    found = false;
                }
            }
            while (found);
            return newWorksheet;
        }

        private Worksheet FindSheet(string name)
        {
            return Globals.ThisAddIn.Application.Worksheets.SheetExists(name);
        }

        private Range GetSelection()
        {
            Range range = Globals.ThisAddIn.Application.Selection;

            return range;
        }

        private void btnTheySay_Click(object sender, RibbonControlEventArgs e)
        {
            Process.Start(Excel.Default.TheySayMainSite);
        }

        private int ProcessDocumentResults(List<Task<ResponseWrapper<SentimentResponse>>> data, List<AnalysisCell> cells, int index)
        {
            try
            {
                LockUi();
                
                Trace.WriteLine("ProcessDocumentResults index: " + index);
                Worksheet newWorksheet = index == 0 ? CreateSheet("Document Sentiment") : FindSheet(_documentSheetName);

                if (index == 0)
                {
                    _documentSheetName = newWorksheet.Name;
                    var entityHeadings = new[]
                                             {
                                                 "Input Cell",
                                                 "Word Count",
                                                 "Label",
                                                 "Positive",
                                                 "Negative",
                                                 "Neutral",
                                                 "Confidence"
                                             };

                    WriteHeadings(newWorksheet, entityHeadings);

                    index = 1;
                }
                else
                {
                    index++; // increment to one beyond because of heading 
                }

                var listData = new object[data.Count,9];
                var startrow = index + 1;
                var rowIndex = 0;
                foreach (var rec in data)
                {
                    var logentry = new LogEntry {ApiCall = "Document Analysis", RequestData = rec.Result.Input};

                    if (rec.Result.Success)
                    {
                        var rsp = rec.Result.Response;
                        int col = 0;

                        listData[rowIndex, col++] = rec.Result.Input.Address;
                        listData[rowIndex, col++] = rsp.wordCount;
                        listData[rowIndex, col++] = rsp.sentiment.label;
                        listData[rowIndex, col++] = rsp.sentiment.positive;
                        listData[rowIndex, col++] = rsp.sentiment.negative;
                        listData[rowIndex, col++] = rsp.sentiment.neutral;
                        listData[rowIndex, col++] = rsp.sentiment.confidence;
                        listData[rowIndex, col++] = "";
                        listData[rowIndex, col] = "";
                    }
                    else
                    {
                        if (rec.Result.ErrorMessage.Contains("429"))
                        {
                            index--;
                            rowIndex--;
                        }
                        else
                        {
                            logentry.ErrorMessage = rec.Result.ErrorMessage;

                            int col = 0;
                            listData[rowIndex, col++] = rec.Result.Input.Address;
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = rec.Result.Input.Text;
                            listData[rowIndex, col] = rec.Result.ErrorMessage;
                        }
                    }
                    Globals.ThisAddIn.LogBuffer.AddEntry(logentry);

                    // increment when fail too - as currently show error in output
                    index++;
                    rowIndex++;
                }
                WriteRange(startrow, startrow + rowIndex, 9, newWorksheet, listData);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                Globals.ThisAddIn.LogBuffer.AddEntry(new LogEntry{ApiCall = "Document Analysis", ErrorMessage = ex.Message});
            }
            finally
            {
                // unlock
                ReEnable();
            }
            return index;
        }

        private int ProcessEntityResults(List<Task<ResponseWrapper<SentimentEntityResponse[]>>> data, List<AnalysisCell> cells, int index)
        {
            try
            {
                LockUi();
                
                Trace.WriteLine("ProcessEntityResults index: " + index);
                Worksheet newWorksheet = index == 0 ? CreateSheet("Entity Sentiment") : FindSheet(_entitySheetName);

                if (index == 0)
                {
                    _entitySheetName = newWorksheet.Name;
                    var entityHeadings = new[]
                                             {
                                                 "Input Cell",
                                                 "Context", 
                                                 "Head Noun",
                                                 "Label",
                                                 "Positive",
                                                 "Negative",
                                                 "Neutral",
                                                 "Confidence"
                                             };

                    WriteHeadings(newWorksheet, entityHeadings);

                    index++;
                    _entityIndex = index;
                }
                else
                {
                    index = 1; // because of heading 
                }

                const int noCols = 10;
                foreach (var rec in data)
                {
                    var logentry = new LogEntry {ApiCall = "Entity Analysis", RequestData = rec.Result.Input};

                    var listData = new object[rec.Result.Response == null ? 1 : rec.Result.Response.Length, noCols];
                    var startrow = _entityIndex + 1;
                    var rowIndex = 0;

                    if (rec.Result.Success)
                    {
                        foreach (var rsp in rec.Result.Response)
                        {
                            int col = 0;

                            listData[rowIndex, col++] = rec.Result.Input.Address;
                            listData[rowIndex, col++] = rsp.sentence;
                            listData[rowIndex, col++] = rsp.headNoun;
                            listData[rowIndex, col++] = rsp.sentiment.label;
                            listData[rowIndex, col++] = rsp.sentiment.positive;
                            listData[rowIndex, col++] = rsp.sentiment.negative;
                            listData[rowIndex, col++] = rsp.sentiment.neutral;
                            listData[rowIndex, col++] = rsp.sentiment.confidence;
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col] = "";

                            rowIndex++;
                            _entityIndex++;
                            index++;
                        }
                    }
                    else
                    {
                        if (!rec.Result.ErrorMessage.Contains("429"))
                        {
                            logentry.ErrorMessage = rec.Result.ErrorMessage;
                            int col = 0;

                            listData[rowIndex, col++] = rec.Result.Input.Address;
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = "";
                            listData[rowIndex, col++] = rec.Result.Input.Text;
                            listData[rowIndex, col] = rec.Result.ErrorMessage;

                            rowIndex++;
                            _entityIndex++;
                            // increment when fail too - as currently show error in output
                            index++;
                        }
                    }
                    Globals.ThisAddIn.LogBuffer.AddEntry(logentry);
                    WriteRange(startrow, startrow + rowIndex, noCols, newWorksheet, listData);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                Globals.ThisAddIn.LogBuffer.AddEntry(new LogEntry { ApiCall = "Entity Analysis", ErrorMessage = ex.Message });
            }
            finally
            {
                // unlock
                ReEnable();
            }

            return index;
        }

        private void btnActivityLog_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.CreateLogPane();
            Globals.ThisAddIn.Pane.RefreshLog();
        }

        private void btnLoginSettings_Click(object sender, RibbonControlEventArgs e)
        {
            var dlgSettings = new UserSettingsDlg();
            var result = dlgSettings.ShowDialog();
            if (result == DialogResult.OK)
            {
                _throttleApi.UpdateSettings(Excel.Default.Username, Excel.Default.Password, Excel.Default.ApiEndpoint);
                Globals.ThisAddIn.LogBuffer.Resize(Excel.Default.LogEntries);
            }
        }

        private void ShowUserError(string message)
        {
            if (message.Contains("401"))
            {
                message = "The credentials in user settings are invalid. Please correct these and try again.";
            }
            MessageBox.Show(message);
        }

        private async void btnAccountStatus_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                var pingResp = await _throttleApi.PingAsync();
                if (!pingResp.Success)
                {
                    Globals.ThisAddIn.LogBuffer.AddEntry(new LogEntry{ApiCall = "ping", ErrorMessage = pingResp.ErrorMessage});
                }
                var dlgSettings = new AccountStatusDlg
                                      {
                                          Data = _throttleApi.GetStatus(), 
                                          ApiStatus = pingResp.Success, 
                                          ApiThrottle = _throttleApi
                                      };
                dlgSettings.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowUserError(ex.Message);
            }

        }
    }
}
