using LibLite.Log.Lite.FileArchive.Formatter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibLite.Log.Lite.Tests.FileArchive.Formatter
{
    [TestClass]
    public class CustomArchiveFileNameFormatterTests
    {
        [TestMethod]
        public void ReturnsCustomStringWithDefaultExtension()
        {
            var formatter = new CustomArchiveFileNameFormatter(() => "test");

            var result = formatter.Format();

            Assert.AreEqual("test.txt", result);
        }

        [TestMethod]
        public void ReturnsCustomStringWithCustomExtension()
        {
            var formatter = new CustomArchiveFileNameFormatter(() => "test", "json");

            var result = formatter.Format();

            Assert.AreEqual("test.json", result);
        }
    }
}
