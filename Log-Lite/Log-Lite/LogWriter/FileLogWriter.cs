using Log_Lite.Builder;
using Log_Lite.FileArchive.Archiver;
using Log_Lite.FileArchive.Checker;
using System;
using System.IO;

namespace Log_Lite.LogWriter
{
    public class FileLogWriter : ILogWriter
    {
        #region Fields
        private string fileName;
        private string directoryPath;
        private string filePath;
        #endregion
        #region Properties
        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                SetFilePath();
                SetPathsInDependencies();
            }
        }
        public string DirectoryPath
        {
            get { return directoryPath; }
            set
            {
                directoryPath = value;
                SetFilePath();
                SetPathsInDependencies();
            }
        }

        public IArchiveNecessityChecker ArchiveNecessityChecker { get; set; }
        public IFileArchiver FileArchiver { get; set; }
        #endregion
        #region Ctors
        public FileLogWriter() : this(Builder())
        { }

        public FileLogWriter(string fileName,
            string directoryPath,
            IArchiveNecessityChecker archiveNecessityChecker,
            IFileArchiver fileArchiver)
        {
            this.fileName = fileName;
            this.directoryPath = directoryPath;
            SetFilePath();

            this.ArchiveNecessityChecker = archiveNecessityChecker;
            this.FileArchiver = fileArchiver;

            SetPathsInDependencies();
        }

        internal FileLogWriter(FileLogWriterBuilder builder)
            : this(builder.FileName,
                  builder.DirectoryPath,
                  builder.ArchiveNecessityChecker,
                  builder.FileArchiver)
        { }
        #endregion
        #region Methods
        private void SetFilePath()
        {
            filePath = directoryPath + fileName;
        }

        private void SetPathsInDependencies()
        {
            ArchiveNecessityChecker?.SetFilePath(filePath);
            FileArchiver?.SetPaths(filePath, directoryPath);
        }

        public void Write(string log)
        {
            try
            {
                if (ArchiveNecessityChecker?.HaveToArchive() == true)
                {
                    FileArchiver?.Archive();
                    ClearLogFile();
                }

                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine(log);
                }
            }
            catch (System.Exception ex)
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
        #endregion
    }
}
