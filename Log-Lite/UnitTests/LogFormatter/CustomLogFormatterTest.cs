﻿using Log_Lite.Enum;
using Log_Lite.LogFormatter;
using Log_Lite.Model;
using Log_Lite.Model.Invoker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.LogFormatter
{
    [TestClass]
    public class CustomLogFormatterTest
    {
        [TestMethod]
        public void ReturnLogSpecifiedInConstructor()
        {
            var logInfo = new LogInfo(LogLevel.INFO, new InvokerModel("class", "method"), "message");
            var formatter = new CustomLogFormatter((info) => $"12!@_{info.Level}_{info.Invoker.Class}_{info.Invoker.Method}_{info.Message}");

            var log = formatter.Format(logInfo);

            var expected = "12!@_INFO_class_method_message";
            Assert.AreEqual(expected, log);
        }
    }
}
