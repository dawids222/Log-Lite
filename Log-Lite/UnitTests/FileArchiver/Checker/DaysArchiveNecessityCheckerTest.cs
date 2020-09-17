using Log_Lite.Enum;
using Log_Lite.FileArchive.Checker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTests.Model.File;

namespace UnitTests.FileArchiver.Checker
{
    [TestClass]
    public class DaysArchiveNecessityCheckerTest
    {
        [TestMethod]
        public void ReturnsTrueWhenFileIsOlderThanGivenThreshold()
        {
            foreach (var value in Enum.GetValues(typeof(TimeUnit)))
            {
                var fileInfo = new MockFileInfo(creationTime: DateTime.Now.AddSeconds(-2 * (int)value));
                var checker = new DaysArchiveNecessityChecker(fileInfo, 1, (TimeUnit)value);

                var haveToArchive = checker.HaveToArchive();
                Assert.IsTrue(haveToArchive);
            }
        }

        public void ReturnsTrueWhenFileIsSameAgeAsGivenThreshold()
        {
            foreach (var value in Enum.GetValues(typeof(TimeUnit)))
            {
                var fileInfo = new MockFileInfo(creationTime: DateTime.Now.AddSeconds(-1 * (int)value));
                var checker = new DaysArchiveNecessityChecker(fileInfo, 1, (TimeUnit)value);

                var haveToArchive = checker.HaveToArchive();
                Assert.IsTrue(haveToArchive);
            }
        }

        [TestMethod]
        public void ReturnsFalseWhenFileIsYoungerThanGivenThreshold()
        {
            foreach (var value in Enum.GetValues(typeof(TimeUnit)))
            {
                var fileInfo = new MockFileInfo(creationTime: DateTime.Now.AddSeconds(1 * (int)value));
                var checker = new DaysArchiveNecessityChecker(fileInfo, 2, (TimeUnit)value);

                var haveToArchive = checker.HaveToArchive();
                Assert.IsFalse(haveToArchive);
            }
        }
    }
}
