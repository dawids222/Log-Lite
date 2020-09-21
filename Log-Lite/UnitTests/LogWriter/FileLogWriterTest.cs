using Log_Lite.Enum;
using Log_Lite.FileArchive.Archiver;
using Log_Lite.LogFormatter;
using Log_Lite.LogWriter;
using Log_Lite.Model;
using Log_Lite.Model.Invoker;
using Log_Lite.Service.File;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.LogWriter
{
    [TestClass]
    public class FileLogWriterTest
    {
        public FileLogWriter Writer { get; private set; }
        public Mock<ILogFormatter> Formatter { get; private set; }
        public Mock<IFileService> Service { get; private set; }
        public Mock<IFileArchiver> Archiver { get; private set; }
        public LogInfo LogInfo { get; private set; }

        private const string FORMATTER_RETURN_VALUE = "FORMATTER RETURN VALUE";

        [TestInitialize]
        public void Before()
        {
            LogInfo = new LogInfo(LogLevel.INFO, new InvokerModel("", ""), "");
            Formatter = new Mock<ILogFormatter>();
            Formatter.Setup(x => x.Format(LogInfo)).Returns(FORMATTER_RETURN_VALUE);
            Service = new Mock<IFileService>();
            Archiver = new Mock<IFileArchiver>();

            Writer = new FileLogWriter("", "", Formatter.Object, Archiver.Object, Service.Object);
        }

        [TestMethod]
        public void WritesFormattedLog()
        {
            Writer.Write(LogInfo);

            Formatter.Verify(x => x.Format(LogInfo), Times.Once);
            Service.Verify(x => x.Append(It.IsAny<string>(), FORMATTER_RETURN_VALUE), Times.Once);
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
            Writer = new FileLogWriter("", "", Formatter.Object, null, Service.Object);

            Writer.Write(LogInfo);
        }
    }
}
