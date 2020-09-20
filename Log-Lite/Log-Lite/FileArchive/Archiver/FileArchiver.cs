using Log_Lite.FileArchive.Checker;
using Log_Lite.Model.File;
using Log_Lite.Service.Directory;
using Log_Lite.Service.File;
using System;

namespace Log_Lite.FileArchive.Archiver
{
    public class FileArchiver : IFileArchiver
    {
        private string ArchiveDirectoryName { get; }
        private IFileInfo FileInfo { get; }
        private IArchiveNecessityChecker Checker { get; }
        private IFileService FileService { get; }
        private IDirectoryService DirectoryService { get; }

        private string ArchiveDirectoryPath { get => $"{FileInfo.DirectoryPath}/{ArchiveDirectoryName}"; }

        public FileArchiver(
            IFileInfo fileInfo,
            string archiveDirectoryName,
            IArchiveNecessityChecker checker)
            : this(fileInfo, archiveDirectoryName, checker,
                  new SystemFileService(), new SystemDirectoryService())
        { }

        public FileArchiver(
            IFileInfo fileInfo,
            string archiveDirectoryName,
            IArchiveNecessityChecker checker,
            IFileService fileService,
            IDirectoryService directoryService)
        {
            ArchiveDirectoryName = archiveDirectoryName;
            FileInfo = fileInfo;
            Checker = checker;
            FileService = fileService;
            DirectoryService = directoryService;
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
            var currentDateTime = DateTime.Now.ToString().Replace(':', '.');
            return $"{ArchiveDirectoryPath}/{currentDateTime}.txt";
        }
    }
}
