using Log_Lite.Enum;
using Log_Lite.FileArchive.Archiver;
using Log_Lite.LogFormatter;
using Log_Lite.LogWriter;
using Log_Lite.Service.File;
using System;
using System.Collections.Generic;

namespace Log_Lite.Builder
{
    public class FileLogWriterBuilder
    {
        public string FileName { get; private set; } = "logs.txt";
        public string DirectoryPath { get; private set; }
            = AppDomain.CurrentDomain.BaseDirectory;
        public IEnumerable<LogLevel> AllowedLogLevels { get; private set; } = null;
        public ILogFormatter Formatter { get; private set; } = new BasicLogFormatter();
        public IFileArchiver FileArchiver { get; private set; } = null;
        public IFileService FileService { get; private set; } = new SystemFileService();


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

        public FileLogWriterBuilder SetAllowedLogLevels(IEnumerable<LogLevel> allowedLogLevels)
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

        public FileLogWriterBuilder SetFileService(IFileService service)
        {
            FileService = service;
            return this;
        }

        public FileLogWriter Create()
        {
            return new FileLogWriter(this);
        }
    }
}
