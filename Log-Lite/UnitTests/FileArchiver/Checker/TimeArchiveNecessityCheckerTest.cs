using Log_Lite.Enum;
using Log_Lite.FileArchive.Checker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTests.Model.File;

namespace UnitTests.FileArchiver.Checker
{
    [TestClass]
    public class TimeArchiveNecessityCheckerTest
    {
        [TestMethod]
        public void ReturnsTrueWhenFileIsOlderThanGivenThreshold()
        {
            foreach (var value in System.Enum.GetValues(typeof(TimeUnit)))
            {
                var fileInfo = new MockFileInfo(creationTime: DateTime.Now.AddSeconds(-2 * (int)value));
                var checker = new TimeArchiveNecessityChecker(fileInfo, 1, (TimeUnit)value);

                var haveToArchive = checker.HaveToArchive();
                Assert.IsTrue(haveToArchive);
            }
        }

        [TestMethod]
        public void ReturnsTrueWhenFileIsSameAgeAsGivenThreshold()
        {
            foreach (var value in System.Enum.GetValues(typeof(TimeUnit)))
            {
                var fileInfo = new MockFileInfo(creationTime: DateTime.Now.AddSeconds(-1 * (int)value));
                var checker = new TimeArchiveNecessityChecker(fileInfo, 1, (TimeUnit)value);

                var haveToArchive = checker.HaveToArchive();
                Assert.IsTrue(haveToArchive);
            }
        }

        [TestMethod]
        public void ReturnsFalseWhenFileIsYoungerThanGivenThreshold()
        {
            foreach (var value in System.Enum.GetValues(typeof(TimeUnit)))
            {
                var fileInfo = new MockFileInfo(creationTime: DateTime.Now.AddSeconds(1 * (int)value));
                var checker = new TimeArchiveNecessityChecker(fileInfo, 2, (TimeUnit)value);

                var haveToArchive = checker.HaveToArchive();
                Assert.IsFalse(haveToArchive);
            }
        }

        [TestMethod]
        public void ReturnsTrueWhenFileIsOlderThanGivenThreshold_DecimalTime()
        {
            var fileInfo = new MockFileInfo(creationTime: DateTime.Now.AddSeconds(-30 * (int)TimeUnit.SECONDS));
            var checker = new TimeArchiveNecessityChecker(fileInfo, 0.5, TimeUnit.MINUTES);

            var haveToArchive = checker.HaveToArchive();
            Assert.IsTrue(haveToArchive);
        }

        [TestMethod]
        public void ReturnsFalseWhenFileIsYoungerThanGivenThreshold_DecimalTime()
        {
            var fileInfo = new MockFileInfo(creationTime: DateTime.Now.AddSeconds(-29 * (int)TimeUnit.SECONDS));
            var checker = new TimeArchiveNecessityChecker(fileInfo, 0.5, TimeUnit.MINUTES);

            var haveToArchive = checker.HaveToArchive();
            Assert.IsFalse(haveToArchive);
        }
    }
}
