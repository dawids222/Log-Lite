using System;
using System.IO;

namespace Log_Lite.FileArchive.Archiver
{
    public class FileArchiver : IFileArchiver
    {
        private string archiveDirectoryName;
        private string fileToArchivePath;
        private string directoryPath;


        public FileArchiver() : this("archive")
        { }

        public FileArchiver(string archiveDirectoryName)
        {
            this.archiveDirectoryName = archiveDirectoryName;
        }


        public void SetPaths(string fileToArchivePath, string directoryPath)
        {
            this.fileToArchivePath = fileToArchivePath;
            this.directoryPath = directoryPath;
        }

        public void Archive()
        {
            try
            {
                var archiveDirectoryPath = directoryPath + archiveDirectoryName;

                if (!Directory.Exists(archiveDirectoryPath))
                    Directory.CreateDirectory(archiveDirectoryPath);

                var archiveFilePath = $"{archiveDirectoryPath}/{CreateArchiveFileName()}.txt";
                File.Copy(fileToArchivePath, archiveFilePath);
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
