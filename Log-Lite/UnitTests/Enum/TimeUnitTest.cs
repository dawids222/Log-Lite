using LibLite.Log.Lite.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibLite.Log.Lite.Tests.Enum
{
    [TestClass]
    public class TimeUnitTest
    {
        [TestMethod]
        public void ElementsHaveTheirRealWorldValues()
        {
            Assert.AreEqual(1, (int)TimeUnit.SECONDS);
            Assert.AreEqual(1 * 60, (int)TimeUnit.MINUTES);
            Assert.AreEqual(1 * 60 * 60, (int)TimeUnit.HOURS);
            Assert.AreEqual(1 * 60 * 60 * 24, (int)TimeUnit.DAYS);
        }
    }
}
