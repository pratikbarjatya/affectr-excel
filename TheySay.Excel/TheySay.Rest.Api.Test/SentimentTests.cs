using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheySay.Rest.Api.Test
{
    [TestClass]
    public class SentimentTests
    {
        private readonly RestBase _subject;

        public SentimentTests()
        {
            // add username / password
            _subject = new RestBase("", "", "http://api.theysay.io");
        }

        [TestMethod]
        public void TestSentimentDocumentLevel()
        {
            var input = new SentimentRequest { text = "Love is a canvas furnished by Nature and embroidered by imagination." };

            var resp = _subject.PostRequest<SentimentResponse, SentimentRequest>(input, "/v1/sentiment", new AnalysisCell{Address = "A1", Text = input.text});

            Assert.IsNotNull(resp.Result);
            Assert.IsTrue(resp.Result.Success);

            Assert.IsTrue(resp.Result.Remaining > 0);
            Assert.IsTrue(resp.Result.Limit > 0);
        }

        [TestMethod]
        public void TestSentimentSentenceLevel()
        {
            var input = new SentimentRequest { text = "Love is a canvas furnished by Nature and embroidered by imagination. This is sentence number two.", level = "sentence"};

            var resp = _subject.PostRequest<SentimentSentenceResponse[], SentimentRequest>(input, "/v1/sentiment", new AnalysisCell{Address = "A1", Text = input.text});

            resp.Wait();
            Assert.IsNotNull(resp.Result);
            Assert.IsTrue(resp.Result.Remaining > 0);
            Assert.IsTrue(resp.Result.Limit > 0);

            var result = resp.Result.Response;
            Assert.AreEqual(2, result.Length);

            Assert.AreEqual(0, result[0].start);
            Assert.AreEqual(11, result[0].end);
            Assert.AreEqual(0, result[0].sentenceIndex);
            Assert.IsTrue(result[0].text.StartsWith("Love is a canvas furnished by Nature and embroidered by imagination"));

            Assert.AreEqual("POSITIVE", result[0].sentiment.label);
            Assert.AreEqual(0.889D, result[0].sentiment.positive);
            Assert.AreEqual(0.0D, result[0].sentiment.negative);
            Assert.AreEqual(0.111, result[0].sentiment.neutral);

            Assert.AreEqual(12, result[1].start);
            Assert.AreEqual(17, result[1].end);
            Assert.AreEqual(1, result[1].sentenceIndex);
            Assert.IsTrue(result[1].text.StartsWith("This is sentence number two"));

            Assert.AreEqual("NEUTRAL", result[1].sentiment.label);
            Assert.AreEqual(0.0D, result[1].sentiment.positive);
            Assert.AreEqual(0.0D, result[1].sentiment.negative);
            Assert.AreEqual(1.0D, result[1].sentiment.neutral);
        }

        [TestMethod]
        public void TestSentimentEntityLevel()
        {
            var input = new SentimentRequest { text = "Love is a canvas furnished by Nature and embroidered by imagination. This is sentence number two.", level = "entity" };

            var resp = _subject.PostRequest<SentimentEntityResponse[], SentimentRequest>(input, "/v1/sentiment", new AnalysisCell{Address = "A1", Text = input.text});

            resp.Wait();
            Assert.IsNotNull(resp.Result);
            Assert.IsTrue(resp.Result.Remaining > 0);
            Assert.IsTrue(resp.Result.Limit > 0);

            var result = resp.Result.Response;
            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void TestInvalidEndpoint()
        {
            var input = new SentimentRequest { text = "Love is a canvas furnished by Nature and embroidered by imagination." };

            var resp = _subject.PostRequest<SentimentResponse, SentimentRequest>(input, "xyz", new AnalysisCell { Address = "A1", Text = input.text });

            resp.Wait();
            Assert.IsNotNull(resp.Result);
            Assert.IsFalse(resp.Result.Success);
            Assert.IsNotNull(resp.Result.ErrorMessage);
            Assert.IsTrue(resp.Result.ErrorMessage.Contains("404"));
        }

        [TestMethod]
        public void TestInvalidCredentials()
        {
            var subject = new RestBase("test", "test", "http://api.theysay.io");

            var input = new SentimentRequest { text = "Love is a canvas furnished by Nature and embroidered by imagination." };

            var resp = subject.PostRequest<SentimentResponse, SentimentRequest>(input, "/v1/sentiment", new AnalysisCell { Address = "A1", Text = input.text });

            resp.Wait();
            Assert.IsNotNull(resp.Result);
            Assert.IsFalse(resp.Result.Success);
            Assert.IsNotNull(resp.Result.ErrorMessage);
            Assert.IsTrue(resp.Result.ErrorMessage.Contains("401"));
        }

        [TestMethod]
        public void TestInvalidRequestContent()
        {
            var input = new SentimentRequest();

            var resp = _subject.PostRequest<SentimentResponse, SentimentRequest>(input, "/v1/sentiment", new AnalysisCell { Address = "A1", Text = input.text });

            resp.Wait();
            Assert.IsNotNull(resp.Result);
            Assert.IsFalse(resp.Result.Success);
            Assert.IsNotNull(resp.Result.ErrorMessage);
            Assert.IsTrue(resp.Result.ErrorMessage.Contains("400"));
        }

        [TestMethod]
        public void BasicPost()
        {
            string respText = null;

            try
            {
                var uri = new Uri("http://api.theysay.io/v1/sentiment");
                var request = (HttpWebRequest)WebRequest.Create(uri);

                // add username / password
                request.Credentials = new NetworkCredential("", "");
                request.Method = "POST";
                request.ContentType = "application/json";

                string postData = JsonHelper.JsonSerialize(new SentimentRequest { text = "Love is a canvas furnished by Nature and embroidered by imagination." });
                var encoding = new ASCIIEncoding();
                byte[] byte1 = encoding.GetBytes(postData);
                request.ContentLength = byte1.Length;

                using (Stream newStream = request.GetRequestStream())
                {
                    newStream.Write(byte1, 0, byte1.Length);
                }

                using(WebResponse v = request.GetResponse())
                using (Stream rStream = v.GetResponseStream())
                {
                    using (var str = new StreamReader(rStream))
                    {
                        if (str.EndOfStream != true)
                        {
                            respText = str.ReadToEnd();
                        }
                    }

                    var des = JsonHelper.JsonDeserialize<SentimentResponse>(respText);

                    var ratelimits = new[]
                                         {
                                             new Limit
                                                 {
                                                     Name = "X-RequestLimit-Remaining",
                                                     LimitType = Limit.ELimit.DayRemaining
                                                 },
                                             new Limit {Name = "X-RequestLimit-Limit", LimitType = Limit.ELimit.DayLimit}
                                         };

                    var wrapper = new ResponseWrapper<SentimentResponse>(des);

                    foreach (var limit in ratelimits)
                    {
                        var vals = v.Headers.GetValues(limit.Name).ToList();
                        wrapper[limit.LimitType] = Convert.ToInt64(vals[0]);
                    }
                    Assert.IsTrue(wrapper.Limit > 0);
                }

            }

            catch (Exception ex)
            {
                var mess = ex.Message;
                Trace.WriteLine(mess);
            }
            Assert.IsNotNull(respText);
        }

    }
}
