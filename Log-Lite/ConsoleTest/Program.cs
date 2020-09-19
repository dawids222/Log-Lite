using Log_Lite.Enum;
using Log_Lite.FileArchive.Archiver;
using Log_Lite.FileArchive.Checker;
using Log_Lite.LogFormatter;
using Log_Lite.Logger;
using Log_Lite.LogWriter;
using Log_Lite.Model.File;
using System;
using System.Threading;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = CreateLogger();
            logger.Info("Cześć");
            logger.Warning("Chyba");
            logger.Error("Logger");
            logger.Fatal("Działa");
            Thread.Sleep(6000);
            logger.Fatal("Powienien się zrobić archive");
            Console.ReadKey();
        }

        static ILogger CreateLogger()
        {
            var fileWriter = CreateBasicFileWriter();
            var errorFileWriter = CreateErrorsFileWriter();
            var consoleWriter = CreateBasicConsoleWriter();
            return new Logger(fileWriter, errorFileWriter, consoleWriter);
        }

        static ILogWriter CreateBasicFileWriter()
        {
            var basicFormatter = new BasicLogFormatter();
            var fileInfo = new SystemFileInfo("logs.txt");
            var checker = new TimeArchiveNecessityChecker(fileInfo, 5, TimeUnit.SECONDS);
            var archiver = new FileArchiver(
                fileInfo,
                "Archive",
                checker);
            return FileLogWriter.Builder()
                    .SetFileName("logs.txt")
                    .SetLogFormatter(basicFormatter)
                    .SetFileArchiver(archiver)
                    .Create();
        }

        static ILogWriter CreateErrorsFileWriter()
        {
            var basicFormatter = new BasicLogFormatter();
            var errorsFileInfo = new SystemFileInfo("errors.txt");
            var errorsChecker = new TimeArchiveNecessityChecker(errorsFileInfo, 5, TimeUnit.SECONDS);
            var errorsArchiver = new FileArchiver(
                errorsFileInfo,
                "Archive_Errors",
                errorsChecker);
            return FileLogWriter.Builder()
                    .SetFileName("errors.txt")
                    .SetLogFormatter(basicFormatter)
                    .SetFileArchiver(errorsArchiver)
                    .SetAllowedLogLevels(new LogLevel[] { LogLevel.ERROR, LogLevel.FATAL })
                    .Create();
        }

        static ILogWriter CreateBasicConsoleWriter()
        {
            var simpleFormatter = new CustomLogFormatter((i) => $"{i.Message}");
            return new ConsoleLogWriter(simpleFormatter);
        }
    }
}
