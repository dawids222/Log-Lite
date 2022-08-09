using LibLite.Log.Lite.FileArchive.Formatter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LibLite.Log.Lite.Tests.FileArchive.Formatter
{
    [TestClass]
    public class MillisecondsArchiveFileNameFormatterTests
    {
        [TestMethod]
        public void ReturnsDatetimeAsMillisecondsWithDefaultExtension()
        {
            var formatter = new MillisecondsArchiveFileNameFormatter();

            var expected = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            var result = formatter.Format();


            Assert.IsTrue(result.Contains(".txt"));
            var milliseconds = long.Parse(result.Split('.')[0]);
            Assert.AreEqual(expected, milliseconds);
        }

        [TestMethod]
        public void ReturnsDatetimeAsMillisecondsWithCustomExtension()
        {
            var formatter = new MillisecondsArchiveFileNameFormatter("json");

            var expected = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            var result = formatter.Format();


            Assert.IsTrue(result.Contains(".json"));
            var milliseconds = long.Parse(result.Split('.')[0]);
            Assert.AreEqual(expected, milliseconds);
        }
    }
}
