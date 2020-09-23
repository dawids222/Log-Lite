using Log_Lite.Builder;
using Log_Lite.Enum;
using Log_Lite.FileArchive.Archiver;
using Log_Lite.LogFormatter;
using Log_Lite.Model;
using Log_Lite.Model.File;
using Log_Lite.Service.Directory;
using Log_Lite.Service.File;
using System.Collections.Generic;

namespace Log_Lite.LogWriter
{
    public class FileLogWriter : BaseLogWriter
    {
        private IFileInfo FileInfo { get; }
        private string FilePath { get => FileInfo.Path; }

        private IFileArchiver FileArchiver { get; }
        private IFileService FileService { get; }
        private IDirectoryService DirectoryService { get; }

        public FileLogWriter() : this(new FileLogWriterBuilder()) { }

        public FileLogWriter(
            IFileInfo fileInfo,
            ILogFormatter formatter,
            IFileArchiver fileArchiver,
            IFileService fileService,
            IDirectoryService directoryService,
            IEnumerable<LogLevel> allowedLogLevels = null
        ) : base(formatter, allowedLogLevels)
        {
            FileInfo = fileInfo;
            FileArchiver = fileArchiver;
            FileService = fileService;
            DirectoryService = directoryService;
        }

        public FileLogWriter(FileLogWriterBuilder builder)
            : this(
                 builder.FileInfo,
                 builder.Formatter,
                 builder.FileArchiver,
                 builder.FileService,
                 builder.DirectoryService,
                 builder.AllowedLogLevels)
        { }

        protected override void WriteWhenAllowed(LogInfo info)
        {
            HandleArchivization();
            HandleDirectoryExistance();
            HandleLogging(info);
        }

        private void HandleArchivization()
        {
            if (FileArchiver?.HaveToArchive() == true)
            {
                FileArchiver?.Archive();
            }
        }

        private void HandleDirectoryExistance()
        {
            if (!DirectoryService.Exists(FileInfo.DirectoryPath))
            {
                DirectoryService.Create(FileInfo.DirectoryPath);
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
