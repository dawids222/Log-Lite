using Log_Lite.LogFormatter;
using Log_Lite.LogWriter;
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

        private void WritesLogToConsole(ILogFormatter formatter)
        {
            var consoleLogWriter = new ConsoleLogWriter(formatter);

            TestConsoleOutput(
                () => consoleLogWriter.Write(null),
                (output) => Assert.AreEqual(formatter.Format(null), output)
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
