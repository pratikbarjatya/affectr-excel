using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheySay.Excel.Test
{
    [TestClass]
    public class LogBufferTest
    {
        private LogBuffer _subject;

        [TestInitialize]
        public void Setup()
        {
            _subject = new LogBuffer(10000);
        }

        [TestMethod]
        public void TestWrap()
        {
            for (var i = 0; i < 10001; i++)
            {
                _subject.AddEntry(new LogEntry{ErrorMessage = i.ToString(CultureInfo.InvariantCulture)});
            }

            Assert.AreEqual(10000, _subject.Entries().Count());
            Assert.AreEqual("1", _subject.Entries().First().ErrorMessage);
            Assert.AreEqual("10000", _subject.Entries().Last().ErrorMessage);
        }

        [TestMethod]
        public void TestResizeShrink()
        {
            for (var i = 0; i < 10000; i++)
            {
                _subject.AddEntry(new LogEntry{ErrorMessage = i.ToString(CultureInfo.InvariantCulture)});
            }

            _subject.Resize(200);
            Assert.AreEqual(200, _subject.Entries().Count());
            Assert.AreEqual("9800", _subject.Entries().First().ErrorMessage);
            Assert.AreEqual("9999", _subject.Entries().Last().ErrorMessage);
        }
    }
}
