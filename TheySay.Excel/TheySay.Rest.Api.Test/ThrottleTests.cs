using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheySay.Rest.Api.Test
{
    [TestClass]
    public class ThrottleTests
    {
        private readonly ApiThrottle _subject;

        public ThrottleTests()
        {
            // add username / password
            _subject = new ApiThrottle("", "", "http://api.theysay.io", 10);
        }

        [TestMethod]
        public void TestSentenceBelowLimit()
        {
            var input = new List<AnalysisCell>();
            for (int i = 0; i < 100; i++)
            {
                input.Add(new AnalysisCell{Address = "A" + i, Text = "This is a test sentence."});
            }
            var output = _subject.AnalyseSentence(input, CallbackFunc, null);

            output.Wait();

            Assert.IsNotNull(output);
        }

        private int CallbackFunc(List<Task<ResponseWrapper<SentimentSentenceResponse[]>>> sentimentSentenceResponses, List<AnalysisCell> cells, int i)
        {
            Trace.WriteLine(string.Format("called with {0} data items and index {1}", sentimentSentenceResponses.Count, i));
            return i;
        }

        [TestMethod]
        public void TestSentenceAboveLimit()
        {
            var input = new List<AnalysisCell>();
            for (int i = 0; i < 350; i++)
            {
                input.Add(new AnalysisCell{Text = "This is a test sentence.", Address = "A" + i});
            }
            var output = _subject.AnalyseSentence(input, CallbackFunc, null);

            output.Wait();

            Assert.IsNotNull(output);
        }

        [TestMethod]
        public void TestDocumentAboveLimit()
        {
            var input = new List<AnalysisCell>();
            for (int i = 0; i < 350; i++)
            {
                input.Add(new AnalysisCell{Text = "This is a test sentence.", Address = "A" + i});
            }
            var output = _subject.AnalyseDocument(input, DocumentCallbackFunc, null);

            output.Wait();

            Assert.IsNotNull(output);
        }

        private int DocumentCallbackFunc(List<Task<ResponseWrapper<SentimentResponse>>> responses, List<AnalysisCell> cells,  int i)
        {
            Trace.WriteLine(string.Format("called with {0} data items and index {1}", responses.Count, i));
            foreach (var response in responses)
            {
                Trace.WriteLine(response.Result.Success);
            }
            var success = from t in responses
                where t.Result.Success
                select t;
            Trace.WriteLine("Success count " + success.Count());
            return i;
        }
    }
}
