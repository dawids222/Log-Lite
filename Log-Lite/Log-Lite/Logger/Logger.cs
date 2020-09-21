using Log_Lite.Enum;
using Log_Lite.LogWriter;
using Log_Lite.Model;
using Log_Lite.Model.Invoker;
using Log_Lite.Provider.Invoker;
using System.Collections.Generic;

namespace Log_Lite.Logger
{
    public class Logger : ILogger
    {
        protected static object lockObject = new object();

        private IEnumerable<ILogWriter> LogWriters { get; }
        private IInvokerModelProvider InvokerModelProvider { get; }
            = new InvokerModelProvider(4);

        public Logger() :
            this(new FileLogWriter())
        { }

        public Logger(params ILogWriter[] logWriters)
            : this((IEnumerable<ILogWriter>)logWriters)
        { }

        public Logger(IEnumerable<ILogWriter> logWriters)
        {
            LogWriters = new List<ILogWriter>(logWriters);
        }

        public void Info(object message)
        {
            Log(message, LogLevel.INFO);
        }

        public void Warning(object message)
        {
            Log(message, LogLevel.WARNING);
        }

        public void Error(object message)
        {
            Log(message, LogLevel.ERROR);
        }

        public void Fatal(object message)
        {
            Log(message, LogLevel.FATAL);
        }

        protected virtual void Log(object message, LogLevel type)
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
            foreach (var writer in LogWriters)
            {
                writer.Write(logInfo);
            }
        }

        protected IInvokerModel GetInvokerInfo()
        {
            return InvokerModelProvider.GetCurrentInvoker();
        }
    }
}