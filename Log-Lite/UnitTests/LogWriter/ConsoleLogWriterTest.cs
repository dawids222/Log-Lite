using Log_Lite.Enum;
using Log_Lite.LogFormatter;
using Log_Lite.LogWriter;
using Log_Lite.Model;
using Log_Lite.Model.Invoker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UnitTests.LogWriter
{
    [TestClass]
    public class ConsoleLogWriterTest
    {
        [TestMethod]
        public void WritesOneLineLogToTheConsole()
        {
            var formatter = new CustomLogFormatter((info) => "the test log");
            WritesLogToConsole(formatter);
        }

        [TestMethod]
        public void WritesMultiLineLogToTheConsole()
        {
            var formatter = new CustomLogFormatter((info) => @"
                        this
                        is
                        multi
                        line
                        log");
            WritesLogToConsole(formatter);
        }

        [TestMethod]
        public void WritesOnlyLogsWithGivenLogLevels()
        {
            var logInfoInfo = new LogInfo(LogType.INFO, new InvokerModel("", ""), "");
            var logInfoWarning = new LogInfo(LogType.WARNING, new InvokerModel("", ""), "");
            var logInfoError = new LogInfo(LogType.ERROR, new InvokerModel("", ""), "");
            var logInfoFatal = new LogInfo(LogType.FATAL, new InvokerModel("", ""), "");
            var formatter = new CustomLogFormatter((i) =>
            {
                if (i.LogType == LogType.FATAL) { Assert.Fail(); }
                return "";
            });
            var allowedLogLevels = new LogType[] { LogType.INFO, LogType.WARNING, LogType.ERROR };
            var logger = new ConsoleLogWriter(formatter, allowedLogLevels);

            logger.Write(logInfoInfo);
            logger.Write(logInfoWarning);
            logger.Write(logInfoError);
            logger.Write(logInfoFatal);
        }

        private void WritesLogToConsole(ILogFormatter formatter)
        {
            var logInfo = new LogInfo(LogType.INFO, new InvokerModel("", ""), "");
            var consoleLogWriter = new ConsoleLogWriter(formatter);

            TestConsoleOutput(
                () => consoleLogWriter.Write(logInfo),
                (output) => Assert.AreEqual(formatter.Format(logInfo), output)
            );
        }

        private void TestConsoleOutput(
            Action test,
            Action<string> assert
        )
        {
            var stringWriter = new StringWriter();
            var originalWriter = Console.Out;
            Console.SetOut(stringWriter);

            test();
            assert(stringWriter.ToString().TrimEnd());

            Console.SetOut(originalWriter);
            stringWriter.Dispose();
        }
    }
}
