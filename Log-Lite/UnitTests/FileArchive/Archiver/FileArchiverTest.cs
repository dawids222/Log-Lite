using Log_Lite.FileArchive.Archiver;
using Log_Lite.FileArchive.Checker;
using Log_Lite.Model.File;
using Log_Lite.Service.Directory;
using Log_Lite.Service.File;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.FileArchive.Archiver
{
    [TestClass]
    public class FileArchiverTest
    {
        public Mock<IFileInfo> FileInfo { get; private set; }
        public Mock<IArchiveNecessityChecker> Checker { get; private set; }
        public Mock<IFileService> FileService { get; private set; }
        public Mock<IDirectoryService> DirectoryService { get; private set; }
        public FileArchiver Archiver { get; private set; }

        [TestInitialize]
        public void Before()
        {
            FileInfo = new Mock<IFileInfo>();
            Checker = new Mock<IArchiveNecessityChecker>();
            FileService = new Mock<IFileService>();
            DirectoryService = new Mock<IDirectoryService>();
            Archiver = new FileArchiver(
                FileInfo.Object, "", Checker.Object, FileService.Object, DirectoryService.Object);
        }

        [TestMethod]
        public void CreatesNewArchiveDirectoryWhenItDoesNotExist()
        {
            DirectoryService.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);

            Archiver.Archive();

            DirectoryService.Verify(x => x.Create(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void DoesNotCreateNewArchiveDirectoryWhenItAlreadyExists()
        {
            DirectoryService.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);

            Archiver.Archive();

            DirectoryService.Verify(x => x.Create(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void MovesCurrectLogFileToArchiveDirectory()
        {
            Archiver.Archive();

            FileService.Verify(x => x.Move(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void FirstHandlesArchiveExistanceThenMovesTheLog()
        {
            var sequence = new MockSequence();
            DirectoryService = new Mock<IDirectoryService>(MockBehavior.Strict);
            FileService = new Mock<IFileService>(MockBehavior.Strict);
            DirectoryService.InSequence(sequence).Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
            DirectoryService.InSequence(sequence).Setup(x => x.Create(It.IsAny<string>()));
            FileService.InSequence(sequence).Setup(x => x.Move(It.IsAny<string>(), It.IsAny<string>()));

            Archiver = new FileArchiver(FileInfo.Object, "", Checker.Object, FileService.Object, DirectoryService.Object);
            Archiver.Archive();

            DirectoryService.Verify(x => x.Exists(It.IsAny<string>()), Times.Once);
            DirectoryService.Verify(x => x.Create(It.IsAny<string>()), Times.Once);
            FileService.Verify(x => x.Move(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ReturnsSameValueAsItsArchiveNecessityChecker()
        {
            Checker.Setup(x => x.HaveToArchive()).Returns(true);
            var result1 = Archiver.HaveToArchive();
            Checker.Setup(x => x.HaveToArchive()).Returns(false);
            var result2 = Archiver.HaveToArchive();

            Assert.IsTrue(result1);
            Assert.IsFalse(result2);
        }
    }
}
