using Log_Lite.Model.File;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UnitTests.Model.File
{
    [TestClass]
    public class SystemFileInfoTest
    {
        private SystemFileInfo LogFileInfo { get; set; }
        private SystemFileInfo NonExistingFileInfo { get; set; }

        private string TestLogFilePath { get; } = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Asset\\log.txt";
        private string NonExistingLogFilePath { get; } = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Asset\\non_existing.txt";

        [TestInitialize]
        public void Before()
        {
            LogFileInfo = new SystemFileInfo(TestLogFilePath);
            NonExistingFileInfo = new SystemFileInfo(NonExistingLogFilePath);
        }

        [TestMethod]
        public void ReturnsFileLegthWhenItExists()
        {
            var bytes = LogFileInfo.Bytes;

            Assert.AreEqual(26, bytes);
        }

        [TestMethod]
        public void ReturnsFileLegth0WhenItDoesNotExist()
        {
            var bytes = NonExistingFileInfo.Bytes;

            Assert.AreEqual(0, bytes);
        }

        [TestMethod]
        public void ReturnsFileCreationTimeWhenItExists()
        {
            var creationDate = LogFileInfo.CreationTime;

            Assert.AreEqual("21.09.2020 21:33:05", creationDate.ToString());
        }

        [TestMethod]
        public void ReturnsNowWhenItDoesNotExist()
        {
            var creationDate = NonExistingFileInfo.CreationTime;

            Assert.AreEqual(DateTime.Now.ToString(), creationDate.ToString());
        }

        [TestMethod]
        public void ReturnsFilePathWhenItExists()
        {
            var path = LogFileInfo.Path;

            Assert.AreEqual(TestLogFilePath, path);
        }

        [TestMethod]
        public void ReturnsFilePathWhenItDoesNotExist()
        {
            var path = NonExistingFileInfo.Path;

            Assert.AreEqual(NonExistingLogFilePath, path);
        }

        [TestMethod]
        public void ReturnsDirectoryNameWhenFileExists()
        {
            var directory = LogFileInfo.DirectoryPath;

            var expected = Path.GetDirectoryName(TestLogFilePath);
            Assert.AreEqual(expected, directory);
        }

        [TestMethod]
        public void ReturnsDirectoryNameWhenFileDoesNotExist()
        {
            var directory = NonExistingFileInfo.DirectoryPath;

            var expected = Path.GetDirectoryName(NonExistingLogFilePath);
            Assert.AreEqual(expected, directory);
        }
    }
}
