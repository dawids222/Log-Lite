using Log_Lite.Enum;
using Log_Lite.FileArchive.Checker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.Model.File;

namespace UnitTests.FileArchiver.Checker
{
    [TestClass]
    public class SizeArchiveNecessityCheckerTest
    {
        [TestMethod]
        public void ReturnsTrueWhenFileBytesAreHigherOrEqual()
        {
            foreach (var value in System.Enum.GetValues(typeof(MemoryUnit)))
            {
                var fileModel = new MockFileInfo((int)value);
                var checker = new SizeArchiveNecessityChecker(fileModel, 1, (MemoryUnit)value);

                var haveToArchive = checker.HaveToArchive();
                Assert.IsTrue(haveToArchive);
            }
        }

        [TestMethod]
        public void ReturnsFalseWhenFileBytesAreSmaller()
        {
            foreach (var value in System.Enum.GetValues(typeof(MemoryUnit)))
            {
                var fileModel = new MockFileInfo((int)value - 1);
                var checker = new SizeArchiveNecessityChecker(fileModel, 1, (MemoryUnit)value);

                var haveToArchive = checker.HaveToArchive();
                Assert.IsFalse(haveToArchive);
            }
        }
    }
}
