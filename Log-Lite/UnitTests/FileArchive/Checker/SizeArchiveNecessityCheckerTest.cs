using LibLite.Log.Lite.Enum;
using LibLite.Log.Lite.FileArchive.Checker;
using LibLite.Log.Lite.Tests.Model.File;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibLite.Log.Lite.Tests.FileArchive.Checker
{
    [TestClass]
    public class SizeArchiveNecessityCheckerTest
    {
        [TestMethod]
        public void ReturnsTrueWhenFileBytesAreHigherOrEqual()
        {
            foreach (var value in System.Enum.GetValues(typeof(MemoryUnit)))
            {
                var fileModel = new MockFileInfo(bytes: (int)value);
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
                var fileModel = new MockFileInfo(bytes: (int)value - 1);
                var checker = new SizeArchiveNecessityChecker(fileModel, 1, (MemoryUnit)value);

                var haveToArchive = checker.HaveToArchive();
                Assert.IsFalse(haveToArchive);
            }
        }

        [TestMethod]
        public void ReturnsTrueWhenFileBytesAreHigherOrEqual_DecimalSize()
        {
            var fileModel = new MockFileInfo(bytes: 512);
            var checker = new SizeArchiveNecessityChecker(fileModel, 0.5, MemoryUnit.KB);

            var haveToArchive = checker.HaveToArchive();
            Assert.IsTrue(haveToArchive);
        }

        [TestMethod]
        public void ReturnsFalseWhenFileBytesAreSmaller_DecimalSize()
        {
            var fileModel = new MockFileInfo(bytes: 511);
            var checker = new SizeArchiveNecessityChecker(fileModel, 0.5, MemoryUnit.KB);

            var haveToArchive = checker.HaveToArchive();
            Assert.IsFalse(haveToArchive);
        }
    }
}
