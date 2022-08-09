using LibLite.Log.Lite.Enum;
using LibLite.Log.Lite.FileArchive.Archiver;
using LibLite.Log.Lite.FileArchive.Checker;
using LibLite.Log.Lite.FileArchive.Formatter;
using LibLite.Log.Lite.LogFormatter;
using LibLite.Log.Lite.Logger;
using LibLite.Log.Lite.LogWriter;
using LibLite.Log.Lite.Model.File;
using System;
using System.Threading;

namespace LibLite.Log.Lite.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = CreateLogger();
            logger.DebugAsync("Halo?");
            logger.InfoAsync("Cześć");
            logger.WarningAsync("Chyba");
            logger.ErrorAsync("Logger");
            logger.FatalAsync("Działa");
            Thread.Sleep(6000);
            logger.FatalAsync("Powienien się zrobić archive");
            Console.ReadKey();
        }

        static ILogger CreateLogger()
        {
            var fileWriter = CreateBasicFileWriter();
            var errorFileWriter = CreateErrorsFileWriter();
            var consoleWriter = CreateBasicConsoleWriter();
            return new Logger.Logger(fileWriter, errorFileWriter, consoleWriter);
        }

        static ILogWriter CreateBasicFileWriter()
        {
            var fileInfo = new SystemFileInfo("logs.txt");
            var checker = new TimeArchiveNecessityChecker(fileInfo, 5, TimeUnit.SECONDS);
            var archiver = new FileArchiver(fileInfo, "Archive", checker);
            return FileLogWriter.Builder()
                    .SetFileInfo(fileInfo)
                    .SetFileArchiver(archiver)
                    .Build();
        }

        static ILogWriter CreateErrorsFileWriter()
        {
            var errorsFileInfo = new SystemFileInfo("errors.txt");
            var errorsChecker = new SizeArchiveNecessityChecker(errorsFileInfo, 100, MemoryUnit.B);
            var fileNameFormatter = new MillisecondsArchiveFileNameFormatter("json");
            var errorsArchiver = new FileArchiver(errorsFileInfo, "Archive_Errors", errorsChecker, fileNameFormatter: fileNameFormatter);
            var logLevels = new LogLevel[] { LogLevel.ERROR, LogLevel.FATAL };
            return FileLogWriter.Builder()
                    .SetFileInfo(errorsFileInfo)
                    .SetFileArchiver(errorsArchiver)
                    .SetAllowedLogLevels(logLevels)
                    .Build();
        }

        static ILogWriter CreateBasicConsoleWriter()
        {
            var simpleFormatter = new CustomLogFormatter((i) => $"{i.Message}");
            return new ConsoleLogWriter(simpleFormatter);
        }
    }
}
