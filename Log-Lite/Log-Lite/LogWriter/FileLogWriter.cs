using Log_Lite.Builder;
using Log_Lite.FileArchive.Archiver;
using Log_Lite.FileArchive.Checker;
using System;
using System.IO;

namespace Log_Lite.LogWriter
{
    public class FileLogWriter : ILogWriter
    {
        private string fileName;
        private string directoryPath;
        private string filePath;

        private IArchiveNecessityChecker archiveNecessityChecker;
        private IFileArchiver fileArchiver;


        public FileLogWriter() : this(Builder())
        { }

        public FileLogWriter(string fileName,
            string directoryPath,
            IArchiveNecessityChecker archiveNecessityChecker,
            IFileArchiver fileArchiver)
        {
            this.fileName = fileName;
            this.directoryPath = directoryPath;
            filePath = directoryPath + fileName;

            this.archiveNecessityChecker = archiveNecessityChecker;
            this.archiveNecessityChecker.SetFilePath(filePath);

            this.fileArchiver = fileArchiver;
            this.fileArchiver.SetPaths(filePath, directoryPath);
        }

        internal FileLogWriter(FileLogWriterBuilder builder)
            : this(builder.FileName,
                  builder.DirectoryPath,
                  builder.ArchiveNecessityChecker,
                  builder.FileArchiver)
        { }


        public void Write(string log)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine(log);
                }

                if (archiveNecessityChecker?.HaveToArchive() == true)
                {
                    fileArchiver?.Archive();
                    ClearLogFile();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ClearLogFile()
        {
            File.WriteAllText(filePath, string.Empty);
        }

        public static FileLogWriterBuilder Builder()
        {
            return new FileLogWriterBuilder();
        }
    }
}
