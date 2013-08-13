using System;
using TheySay.Rest.Api;

namespace TheySay.Excel
{
    public class LogEntry
    {
        public LogEntry()
        {
            Time = DateTime.Now;
        }

        public string ApiCall { get; set; }
        public AnalysisCell RequestData { get; set; }
        public DateTime Time { get; private set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                return RequestData == null
                           ? string.Format("Time: {0} Call: {1} Error: {2}", Time.ToShortTimeString(), ApiCall,
                                           ErrorMessage)
                           : string.Format("Time: {0} Call: {1}  Cell:{2} Request Text: {3} Error: {4}",
                                           Time.ToShortTimeString(), ApiCall, RequestData.Address, RequestData.Text,
                                           ErrorMessage);
            }
            return string.Format("Time: {0} Call: {1} Request - Cell:{2} Text: {3}", Time.ToShortTimeString(), ApiCall, RequestData.Address, RequestData.Text);
        }
    }
}