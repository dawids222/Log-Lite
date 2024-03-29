﻿using LibLite.Log.Lite.Enum;
using LibLite.Log.Lite.LogWriter;
using LibLite.Log.Lite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace LibLite.Log.Lite.Tests.Logger
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void CallsWriteOnEveryWriter()
        {
            var writers = new List<Mock<ILogWriter>>(10);
            for (var i = 0; i < 10; i++) { writers.Add(new Mock<ILogWriter>()); }
            var objects = writers.Select(x => x.Object);
            var logger = new Lite.Logger.Logger(objects);

            logger.Info("message");

            foreach (var writer in writers)
            {
                writer.Verify(x => x.Write(It.IsAny<LogInfo>()), Times.Once);
            }
        }

        [TestMethod]
        public void CapturesCorrectLogInfo()
        {
            var writer = new Mock<ILogWriter>();
            LogInfo logInfo = null;
            writer
                .Setup(x => x.Write(It.IsAny<LogInfo>()))
                .Callback((LogInfo x) => logInfo = x);
            var logger = new Lite.Logger.Logger(writer.Object);

            logger.Info("MESSAGE");

            Assert.AreEqual(LogLevel.INFO, logInfo.Level);
            Assert.AreEqual("LoggerTest", logInfo.Invoker.Class);
            Assert.AreEqual("CapturesCorrectLogInfo", logInfo.Invoker.Method);
            Assert.AreEqual("MESSAGE", logInfo.Message);
        }
    }
}
