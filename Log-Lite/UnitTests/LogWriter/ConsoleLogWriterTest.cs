﻿using LibLite.Log.Lite.Enum;
using LibLite.Log.Lite.LogFormatter;
using LibLite.Log.Lite.LogWriter;
using LibLite.Log.Lite.Model;
using LibLite.Log.Lite.Model.Invoker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace LibLite.Log.Lite.Tests.LogWriter
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
            var logInfoInfo = new LogInfo(LogLevel.INFO, new InvokerModel("", ""), "");
            var logInfoWarning = new LogInfo(LogLevel.WARNING, new InvokerModel("", ""), "");
            var logInfoError = new LogInfo(LogLevel.ERROR, new InvokerModel("", ""), "");
            var logInfoFatal = new LogInfo(LogLevel.FATAL, new InvokerModel("", ""), "");
            var formatter = new CustomLogFormatter((i) =>
            {
                if (i.Level == LogLevel.FATAL) { Assert.Fail(); }
                return "";
            });
            var allowedLogLevels = new LogLevel[] { LogLevel.INFO, LogLevel.WARNING, LogLevel.ERROR };
            var logger = new ConsoleLogWriter(formatter, allowedLogLevels);

            logger.Write(logInfoInfo);
            logger.Write(logInfoWarning);
            logger.Write(logInfoError);
            logger.Write(logInfoFatal);
        }

        private void WritesLogToConsole(ILogFormatter formatter)
        {
            var logInfo = new LogInfo(LogLevel.INFO, new InvokerModel("", ""), "");
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
