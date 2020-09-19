using Log_Lite.FileArchive.Checker;
using Log_Lite.Model.File;
using System;
using System.IO;

namespace Log_Lite.FileArchive.Archiver
{
    public class FileArchiver : IFileArchiver
    {
        private string ArchiveDirectoryName { get; }
        private IFileInfo FileInfo { get; }
        private IArchiveNecessityChecker Checker { get; }

        private string ArchiveDirectoryPath { get => $"{FileInfo.DirectoryPath}/{ArchiveDirectoryName}"; }

        public FileArchiver(
            IFileInfo fileInfo,
            string archiveDirectoryName,
            IArchiveNecessityChecker checker)
        {
            ArchiveDirectoryName = archiveDirectoryName;
            FileInfo = fileInfo;
            Checker = checker;
        }

        public bool HaveToArchive()
        {
            return Checker.HaveToArchive();
        }

        public void Archive()
        {
            if (!Directory.Exists(ArchiveDirectoryPath))
                Directory.CreateDirectory(ArchiveDirectoryPath);

            var archiveFilePath = CreateArchiveFilePath();
            File.Move(FileInfo.Path, archiveFilePath);
        }

        private string CreateArchiveFilePath()
        {
            var currentDateTime = DateTime.Now.ToString().Replace(':', '.');
            return $"{ArchiveDirectoryPath}/{currentDateTime}.txt";
        }
    }
}
