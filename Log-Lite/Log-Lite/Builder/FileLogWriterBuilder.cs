using Log_Lite.Enum;
using Log_Lite.FileArchive.Archiver;
using Log_Lite.LogFormatter;
using Log_Lite.LogWriter;
using System;
using System.Collections.Generic;

namespace Log_Lite.Builder
{
    public class FileLogWriterBuilder
    {
        public string FileName { get; private set; } = "Log.txt";
        public string DirectoryPath { get; private set; }
            = AppDomain.CurrentDomain.BaseDirectory;
        public IEnumerable<LogType> AllowedLogLevels { get; private set; } = null;
        public ILogFormatter Formatter { get; private set; }
        public IFileArchiver FileArchiver { get; private set; }


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

        public FileLogWriterBuilder SetAllowedLogLevels(IEnumerable<LogType> allowedLogLevels)
        {
            AllowedLogLevels = allowedLogLevels;
            return this;
        }
        public FileLogWriterBuilder SetLogFormatter(ILogFormatter formatter)
        {
            Formatter = formatter;
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
