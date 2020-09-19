using Log_Lite.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests.Enum
{
    [TestClass]
    public class MemoryUnitTest
    {
        [TestMethod]
        public void SubsequentValuesArePowersOf1024()
        {
            var values = System.Enum.GetValues(typeof(MemoryUnit));
            for (var i = 0; i < values.Length; i++)
            {
                var expected = Math.Pow(1024, i);
                Assert.AreEqual(expected, (int)values.GetValue(i));
            }
        }

        [TestMethod]
        public void ElementsHaveTheirRealWorldValues()
        {
            Assert.AreEqual(Math.Pow(1024, 0), (int)MemoryUnit.B);
            Assert.AreEqual(Math.Pow(1024, 1), (int)MemoryUnit.KB);
            Assert.AreEqual(Math.Pow(1024, 2), (int)MemoryUnit.MB);
            Assert.AreEqual(Math.Pow(1024, 3), (int)MemoryUnit.GB);
        }
    }
}
