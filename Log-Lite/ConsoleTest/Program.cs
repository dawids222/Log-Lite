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
            logger.Info("Chyba");
            logger.Info("Logger");
            logger.Info("Działa");
            Thread.Sleep(6000);
            logger.Warning("Powienien się zrobić archive");
            Console.ReadKey();
        }

        static ILogger CreateLogger()
        {
            var basicFormatter = new BasicLogFormatter();
            var fileInfo = new SystemFileInfo("logs.txt");
            var checker = new TimeArchiveNecessityChecker(fileInfo, 5, TimeUnit.SECONDS);
            var archiver = new FileArchiver(
                fileInfo,
                "Archive",
                checker);
            var fileWriter = FileLogWriter.Builder()
                    .SetFileName("logs.txt")
                    .SetLogFormatter(basicFormatter)
                    .SetFileArchiver(archiver)
                    .Create();
            var simpleFormatter = new CustomLogFormatter((i) => $"{i.Message}");
            var consoleWriter = new ConsoleLogWriter(simpleFormatter);
            return new Logger(fileWriter, consoleWriter);
        }
    }
}
