using LibLite.Log.Lite.FileArchive.Formatter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LibLite.Log.Lite.Tests.FileArchive.Formatter
{
    [TestClass]
    public class DateTimeArchiveFileNameFormatterTests
    {
        [TestMethod]
        public void ReturnsDatetimeStringWithDefaultExtension()
        {
            var formatter = new DateTimeArchiveFileNameFormatter();

            var result = formatter.Format();

            Assert.IsTrue(result.Contains(".txt"));
            var datetime = result.Split(".txt")[0];
            Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-ddTHH.mm.ss"), datetime);
        }

        [TestMethod]
        public void ReturnsDatetimeStringWithCustomExtension()
        {
            var formatter = new DateTimeArchiveFileNameFormatter("json");

            var result = formatter.Format();

            Assert.IsTrue(result.Contains(".json"));
            var datetime = result.Split(".json")[0];
            Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-ddTHH.mm.ss"), datetime);
        }
    }
}
