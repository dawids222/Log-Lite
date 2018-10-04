using System;
using System.IO;

namespace Log_Lite.FileArchive.Archiver
{
    public class FileArchiver : IFileArchiver
    {
        public string ArchiveDirectoryName { get; private set; }
        public string FileToArchivePath { get; private set; }
        public string DirectoryPath { get; private set; }


        public FileArchiver() : this("archive")
        { }

        public FileArchiver(string archiveDirectoryName)
        {
            this.ArchiveDirectoryName = archiveDirectoryName;
        }


        public void SetPaths(string fileToArchivePath, string directoryPath)
        {
            this.FileToArchivePath = fileToArchivePath;
            this.DirectoryPath = directoryPath;
        }

        public void Archive()
        {
            try
            {
                var archiveDirectoryPath = DirectoryPath + ArchiveDirectoryName;

                if (!Directory.Exists(archiveDirectoryPath))
                    Directory.CreateDirectory(archiveDirectoryPath);

                var archiveFilePath = $"{archiveDirectoryPath}/{CreateArchiveFileName()}.txt";
                File.Copy(FileToArchivePath, archiveFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string CreateArchiveFileName()
        {
            return DateTime.Now.ToString().Replace(':', '.');
        }
    }
}
