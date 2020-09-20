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
        FileLogWriter Writer;
        Mock<ILogFormatter> Formatter;
        Mock<IFileService> Service;
        Mock<IFileArchiver> Archiver;
        LogInfo LogInfo;


        [TestInitialize]
        public void Before()
        {
            Formatter = new Mock<ILogFormatter>();
            Service = new Mock<IFileService>();
            Archiver = new Mock<IFileArchiver>();
            LogInfo = new LogInfo(LogLevel.INFO, new InvokerModel("", ""), "");
            Writer = new FileLogWriter("", "", Formatter.Object, Archiver.Object, Service.Object);
        }

        [TestMethod]
        public void FormatsLog()
        {
            Writer.Write(LogInfo);

            Formatter.Verify(x => x.Format(It.IsAny<LogInfo>()), Times.Once);
        }

        [TestMethod]
        public void WritesLog()
        {
            Writer.Write(LogInfo);

            Service.Verify(x => x.Append(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
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
