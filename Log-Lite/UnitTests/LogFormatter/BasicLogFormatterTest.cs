using LibLite.Log.Lite.Enum;
using LibLite.Log.Lite.LogFormatter;
using LibLite.Log.Lite.Model;
using LibLite.Log.Lite.Model.Invoker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LibLite.Log.Lite.Tests.LogFormatter
{
    [TestClass]
    public class BasicLogFormatterTest
    {
        private ILogFormatter Formatter { get; set; }

        private LogInfo LogInfoInfo { get; } = new LogInfo(LogLevel.INFO, new InvokerModel("INFO", "INFO"), "INFO");
        private LogInfo LogInfoWarning { get; } = new LogInfo(LogLevel.WARNING, new InvokerModel("WARNING", "WARNING"), "WARNING");
        private LogInfo LogInfoError { get; } = new LogInfo(LogLevel.ERROR, new InvokerModel("ERROR", "ERROR"), "ERROR");
        private LogInfo LogInfoFatal { get; } = new LogInfo(LogLevel.FATAL, new InvokerModel("FATAL", "FATAL"), "FATAL");

        [TestInitialize]
        public void Before()
        {
            Formatter = new BasicLogFormatter();
        }

        [TestMethod]
        public void ReturnsProperlyFormatedLog()
        {
            var infoLog = Formatter.Format(LogInfoInfo);
            var warningLog = Formatter.Format(LogInfoWarning);
            var errorLog = Formatter.Format(LogInfoError);
            var fatalLog = Formatter.Format(LogInfoFatal);

            Assert.AreEqual($"{DateTime.Now}   {LogInfoInfo.Level}      {LogInfoInfo.Invoker}  ->  {LogInfoInfo.Message}", infoLog);
            Assert.AreEqual($"{DateTime.Now}   {LogInfoWarning.Level}   {LogInfoWarning.Invoker}  ->  {LogInfoWarning.Message}", warningLog);
            Assert.AreEqual($"{DateTime.Now}   {LogInfoError.Level}     {LogInfoError.Invoker}  ->  {LogInfoError.Message}", errorLog);
            Assert.AreEqual($"{DateTime.Now}   {LogInfoFatal.Level}     {LogInfoFatal.Invoker}  ->  {LogInfoFatal.Message}", fatalLog);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ThrowsWhenInputIsNull()
        {
            Formatter.Format(null);
        }
    }
}
