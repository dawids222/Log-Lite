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

        public List<ILogWriter> LogWriters { get; set; }
        public IInvokerModelProvider InvokerModelProvider { get; set; } = new InvokerModelProvider();


        #region ctors
        public Logger() :
            this(new FileLogWriter())
        { }


        public Logger(params ILogWriter[] logWriters)
        {
            LogWriters = new List<ILogWriter>(logWriters);
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