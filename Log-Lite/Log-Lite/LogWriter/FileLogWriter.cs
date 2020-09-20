using Log_Lite.Builder;
using Log_Lite.Enum;
using Log_Lite.FileArchive.Archiver;
using Log_Lite.LogFormatter;
using Log_Lite.Model;
using Log_Lite.Service.File;
using System.Collections.Generic;

namespace Log_Lite.LogWriter
{
    public class FileLogWriter : BaseLogWriter
    {
        private string FileName { get; }
        private string DirectoryPath { get; }
        private string FilePath { get => $"{DirectoryPath}{FileName}"; }

        private IFileArchiver FileArchiver { get; }
        private IFileService FileService { get; }

        public FileLogWriter() : this(Builder())
        { }

        public FileLogWriter(
            string fileName,
            string directoryPath,
            ILogFormatter formatter,
            IFileArchiver fileArchiver,
            IFileService fileService,
            IEnumerable<LogLevel> allowedLogLevels = null
        ) : base(formatter, allowedLogLevels)
        {
            FileName = fileName;
            DirectoryPath = directoryPath;
            FileArchiver = fileArchiver;
            FileService = fileService;
        }

        internal FileLogWriter(FileLogWriterBuilder builder)
            : this(builder.FileName,
                  builder.DirectoryPath,
                  builder.Formatter,
                  builder.FileArchiver,
                  builder.FileService,
                  builder.AllowedLogLevels)
        { }

        protected override void WriteWhenAllowed(LogInfo info)
        {
            HandleArchivization();
            HandleLogging(info);
        }

        private void HandleArchivization()
        {
            if (FileArchiver?.HaveToArchive() == true)
            {
                FileArchiver?.Archive();
            }
        }

        private void HandleLogging(LogInfo info)
        {
            var log = Formatter.Format(info);
            FileService.Append(FilePath, log);
        }

        public static FileLogWriterBuilder Builder()
        {
            return new FileLogWriterBuilder();
        }
    }
}
