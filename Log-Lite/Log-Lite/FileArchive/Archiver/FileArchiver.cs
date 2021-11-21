using Log_Lite.FileArchive.Checker;
using Log_Lite.FileArchive.Formatter;
using Log_Lite.Model.File;
using Log_Lite.Service.Directory;
using Log_Lite.Service.File;

namespace Log_Lite.FileArchive.Archiver
{
    public class FileArchiver : IFileArchiver
    {
        private string ArchiveDirectoryName { get; }
        private IFileInfo FileInfo { get; }
        private IArchiveNecessityChecker Checker { get; }
        private IFileService FileService { get; }
        private IDirectoryService DirectoryService { get; }
        private IArchiveFileNameFormatter FileNameFormatter { get; }

        private string ArchiveDirectoryPath { get => $"{FileInfo.DirectoryPath}/{ArchiveDirectoryName}"; }

        public FileArchiver(
            IFileInfo fileInfo,
            string archiveDirectoryName,
            IArchiveNecessityChecker checker,
            IFileService fileService = null,
            IDirectoryService directoryService = null,
            IArchiveFileNameFormatter fileNameFormatter = null)
        {
            ArchiveDirectoryName = archiveDirectoryName;
            FileInfo = fileInfo;
            Checker = checker;
            FileService = fileService ?? new SystemFileService();
            DirectoryService = directoryService ?? new SystemDirectoryService();
            FileNameFormatter = fileNameFormatter ?? new DateTimeArchiveFileNameFormatter();
        }

        public bool HaveToArchive()
        {
            return Checker.HaveToArchive();
        }

        public void Archive()
        {
            if (!DirectoryService.Exists(ArchiveDirectoryPath))
                DirectoryService.Create(ArchiveDirectoryPath);

            var archiveFilePath = CreateArchiveFilePath();
            FileService.Move(FileInfo.Path, archiveFilePath);
        }

        private string CreateArchiveFilePath()
        {
            return $"{ArchiveDirectoryPath}/{FileNameFormatter.Format()}";
        }
    }
}
