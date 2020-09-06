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
            WritesLogToConsole("the test log");
        }

        [TestMethod]
        public void WritesMultiLineLogToTheConsole()
        {
            WritesLogToConsole(@"
                        this
                        is
                        multi
                        line
                        log");
        }

        private void WritesLogToConsole(string log)
        {
            var consoleLogWriter = new ConsoleLogWriter();

            TestConsoleOutput(
                () => consoleLogWriter.Write(log),
                (output) => Assert.AreEqual(log, output)
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
