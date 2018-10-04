using Log_Lite.Enum;
using Log_Lite.LogCreator;
using Log_Lite.LogWriter;
using Log_Lite.Model;
using System.Collections.Generic;

namespace Log_Lite.Logger
{
    public class Logger : ILogger
    {
        protected static object lockObject = new object();

        public ILogCreator LogCreator { get; set; }
        public List<ILogWriter> LogWriters { get; set; }


        #region ctors
        public Logger() :
            this(new LogCreator.LogCreator(), new FileLogWriter())
        { }

        public Logger(params ILogWriter[] logWriters) :
            this(new LogCreator.LogCreator(), logWriters)
        { }

        public Logger(ILogCreator logCreator) :
            this(logCreator, new FileLogWriter())
        { }

        public Logger(ILogCreator logCreator, params ILogWriter[] logWriters)
        {
            this.LogCreator = logCreator;
            this.LogWriters = new List<ILogWriter>(logWriters);
        }
        #endregion


        public void Error(object message)
        {
            Log(message, LogType.ERROR);
        }

        public void Fatal(object message)
        {
            Log(message, LogType.FATAL);
        }

        public void Info(object message)
        {
            Log(message, LogType.INFO);
        }

        public void Warning(object message)
        {
            Log(message, LogType.WARNING);
        }

        protected virtual void Log(object message, LogType type)
        {
            lock (lockObject)
            {
                var invokerInfo = GetInvokerInfo();
                var logInfo = new LogInfo(type, invokerInfo, message);
                HandleLogging(logInfo);
            }
        }

        protected void HandleLogging(LogInfo logInfo)
        {
            var log = LogCreator.Create(logInfo);

            foreach (var writer in LogWriters)
            {
                writer.Write(log);
            }
        }

        protected IInvokerModel GetInvokerInfo()
        {
            return new InvokerModel();
        }
    }
}