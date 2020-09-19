using Log_Lite.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Enum
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
