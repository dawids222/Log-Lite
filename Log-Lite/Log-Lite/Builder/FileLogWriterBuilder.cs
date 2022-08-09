using LibLite.Log.Lite.Enum;
using LibLite.Log.Lite.FileArchive.Archiver;
using LibLite.Log.Lite.LogFormatter;
using LibLite.Log.Lite.LogWriter;
using LibLite.Log.Lite.Model.File;
using LibLite.Log.Lite.Service.Directory;
using LibLite.Log.Lite.Service.File;
using System.Collections.Generic;

namespace LibLite.Log.Lite.Builder
{
    public class FileLogWriterBuilder
    {
        public IFileInfo FileInfo { get; private set; } = new SystemFileInfo("logs.txt");
        public IEnumerable<LogLevel> AllowedLogLevels { get; private set; } = null;
        public ILogFormatter Formatter { get; private set; } = new BasicLogFormatter();
        public IFileArchiver FileArchiver { get; private set; } = null;
        public IFileService FileService { get; private set; } = new SystemFileService();
        public IDirectoryService DirectoryService { get; private set; } = new SystemDirectoryService();

        internal FileLogWriterBuilder()
        { }

        public FileLogWriterBuilder SetFileInfo(IFileInfo fileInfo)
        {
            FileInfo = fileInfo;
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

        public FileLogWriterBuilder SetDirectoryService(IDirectoryService service)
        {
            DirectoryService = service;
            return this;
        }

        public FileLogWriter Build()
        {
            return new FileLogWriter(this);
        }
    }
}
