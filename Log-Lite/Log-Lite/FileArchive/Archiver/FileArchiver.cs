using LibLite.Log.Lite.FileArchive.Checker;
using LibLite.Log.Lite.FileArchive.Formatter;
using LibLite.Log.Lite.Model.File;
using LibLite.Log.Lite.Service.Directory;
using LibLite.Log.Lite.Service.File;
using System.Threading.Tasks;

namespace LibLite.Log.Lite.FileArchive.Archiver
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

        public async Task ArchiveAsync()
        {
            if (!await DirectoryService.ExistsAsync(ArchiveDirectoryPath))
                await DirectoryService.CreateAsync(ArchiveDirectoryPath);

            var archiveFilePath = CreateArchiveFilePath();
            await FileService.MoveAsync(FileInfo.Path, archiveFilePath);
        }

        private string CreateArchiveFilePath()
        {
            return $"{ArchiveDirectoryPath}/{FileNameFormatter.Format()}";
        }
    }
}
