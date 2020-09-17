using Log_Lite.FileArchive.Archiver;
using Log_Lite.FileArchive.Checker;
using Log_Lite.LogWriter;
using System;

namespace Log_Lite.Builder
{
    public class FileLogWriterBuilder
    {
        public string FileName { get; private set; } = "Log.txt";
        public string DirectoryPath { get; private set; }
            = AppDomain.CurrentDomain.BaseDirectory;
        public IArchiveNecessityChecker ArchiveNecessityChecker { get; private set; }
        public IFileArchiver FileArchiver { get; private set; }
            = new FileArchiver();


        internal FileLogWriterBuilder()
        { }


        public FileLogWriterBuilder SetFileName(string name)
        {
            FileName = name;
            return this;
        }

        public FileLogWriterBuilder SetDirectoryPath(string path)
        {
            DirectoryPath = path;
            return this;
        }

        public FileLogWriterBuilder SetArchiveNecessityChecker(IArchiveNecessityChecker checker)
        {
            ArchiveNecessityChecker = checker;
            return this;
        }

        public FileLogWriterBuilder SetFileArchiver(IFileArchiver archier)
        {
            FileArchiver = archier;
            return this;
        }

        public FileLogWriter Create()
        {
            return new FileLogWriter(this);
        }
    }
}
