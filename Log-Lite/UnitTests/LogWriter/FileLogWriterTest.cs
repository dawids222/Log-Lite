using LibLite.Log.Lite.Enum;
using LibLite.Log.Lite.FileArchive.Archiver;
using LibLite.Log.Lite.LogFormatter;
using LibLite.Log.Lite.LogWriter;
using LibLite.Log.Lite.Model;
using LibLite.Log.Lite.Model.File;
using LibLite.Log.Lite.Model.Invoker;
using LibLite.Log.Lite.Service.Directory;
using LibLite.Log.Lite.Service.File;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using LibLite.Log.Lite.Tests.Model.File;

namespace LibLite.Log.Lite.Tests.LogWriter
{
    [TestClass]
    public class FileLogWriterTest
    {
        public FileLogWriter Writer { get; private set; }
        public IFileInfo FileInfo { get; private set; }
        public Mock<ILogFormatter> Formatter { get; private set; }
        public Mock<IFileService> FileService { get; private set; }
        public Mock<IDirectoryService> DirectoryServie { get; private set; }
        public Mock<IFileArchiver> Archiver { get; private set; }
        public LogInfo LogInfo { get; private set; }

        private const string FORMATTER_RETURN_VALUE = "FORMATTER RETURN VALUE";

        [TestInitialize]
        public void Before()
        {
            LogInfo = new LogInfo(LogLevel.INFO, new InvokerModel("", ""), "");
            FileInfo = new MockFileInfo("", "");
            Formatter = new Mock<ILogFormatter>();
            Formatter.Setup(x => x.Format(LogInfo)).Returns(FORMATTER_RETURN_VALUE);
            FileService = new Mock<IFileService>();
            DirectoryServie = new Mock<IDirectoryService>();
            DirectoryServie.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            Archiver = new Mock<IFileArchiver>();

            Writer = new FileLogWriter(FileInfo, Formatter.Object, Archiver.Object, FileService.Object, DirectoryServie.Object);
        }

        [TestMethod]
        public void WritesFormattedLog()
        {
            Writer.Write(LogInfo);

            Formatter.Verify(x => x.Format(LogInfo), Times.Once);
            FileService.Verify(x => x.Append(It.IsAny<string>(), FORMATTER_RETURN_VALUE), Times.Once);
        }

        [TestMethod]
        public void ArchivesLogFileWhenItsRequired()
        {
            Archiver.Setup(x => x.HaveToArchive()).Returns(true);

            Writer.Write(LogInfo);

            Archiver.Verify(x => x.Archive(), Times.Once);
        }

        [TestMethod]
        public void DoesNotArchiveLogFileWhenItsNotRequired()
        {
            Archiver.Setup(x => x.HaveToArchive()).Returns(false);

            Writer.Write(LogInfo);

            Archiver.Verify(x => x.Archive(), Times.Never);
        }

        [TestMethod]
        public void WorksWithoutArchiver()
        {
            Writer = new FileLogWriter(FileInfo, Formatter.Object, null, FileService.Object, DirectoryServie.Object);

            Writer.Write(LogInfo);
        }

        [TestMethod]
        public void CreatesDirecotryForLogFileIfItDoesNotExist()
        {
            DirectoryServie.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);

            Writer.Write(LogInfo);

            DirectoryServie.Verify(x => x.Create(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void DoesNotCreateDirecotryForLogFileIfItAlreadyExists()
        {
            Writer.Write(LogInfo);

            DirectoryServie.Verify(x => x.Create(It.IsAny<string>()), Times.Never);
        }
    }
}
