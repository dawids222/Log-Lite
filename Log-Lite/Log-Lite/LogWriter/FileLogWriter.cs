using LibLite.Log.Lite.Builder;
using LibLite.Log.Lite.Enum;
using LibLite.Log.Lite.FileArchive.Archiver;
using LibLite.Log.Lite.LogFormatter;
using LibLite.Log.Lite.Model;
using LibLite.Log.Lite.Model.File;
using LibLite.Log.Lite.Service.Directory;
using LibLite.Log.Lite.Service.File;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibLite.Log.Lite.LogWriter
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

        protected override async Task WriteWhenAllowedAsync(LogInfo info)
        {
            await HandleArchivizationAsync();
            await HandleDirectoryExistanceAsync();
            await HandleLoggingAsync(info);
        }

        private Task HandleArchivizationAsync()
        {
            if (FileArchiver?.HaveToArchive() != true)
                return Task.CompletedTask;
            return FileArchiver?.ArchiveAsync();
        }

        private async Task HandleDirectoryExistanceAsync()
        {
            if (!await DirectoryService.ExistsAsync(FileInfo.DirectoryPath))
            {
                await DirectoryService.CreateAsync(FileInfo.DirectoryPath);
            }
        }

        private async Task HandleLoggingAsync(LogInfo info)
        {
            var log = Formatter.Format(info);
            await FileService.AppendAsync(FilePath, log);
        }

        public static FileLogWriterBuilder Builder()
        {
            return new FileLogWriterBuilder();
        }
    }
}
