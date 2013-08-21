using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheySay.Rest.Api.Test
{
    [TestClass]
    public class RateTests
    {
        private readonly RestBase _subject;

        public RateTests()
        {
            // Add usernmae / password
            _subject = new RestBase("", "", "http://api.theysay.io");
        }
        
        [TestMethod]
        public void TestPing()
        {
            var resp = _subject.Get("/ping");

            resp.Wait();
            Assert.IsTrue(resp.Result.Success);
            Assert.IsNotNull(resp.Result);
        }

        [TestMethod]
        public void TestRateLevelFailsWrongMethod()
        {
            var resp = _subject.PostRequest<RateResponse>("/rate_limit");

            resp.Wait();
            Assert.IsNotNull(resp.Result);
            Assert.IsFalse(resp.Result.Success);
            Assert.IsTrue(resp.Result.ErrorMessage.Contains("405"));
        }

        [TestMethod]
        public void TestRateLevel()
        {
            var resp = _subject.GetRequest<RateResponse>("/rate_limit");

            resp.Wait();

            Assert.IsNotNull(resp.Result);
            Assert.IsTrue(resp.Result.Success);
        }
    }
}
