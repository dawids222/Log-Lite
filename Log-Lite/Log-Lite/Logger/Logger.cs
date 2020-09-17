using Log_Lite.Enum;
using Log_Lite.LogFormatter;
using Log_Lite.LogWriter;
using Log_Lite.Model;
using Log_Lite.Provider.InvokerModel;
using System.Collections.Generic;

namespace Log_Lite.Logger
{
    public class Logger : ILogger
    {
        protected static object lockObject = new object();

        public ILogFormatter LogFormatter { get; set; }
        public List<ILogWriter> LogWriters { get; set; }
        public IInvokerModelProvider InvokerModelProvider { get; set; } = new InvokerModelProvider();


        #region ctors
        public Logger() :
            this(new BasicLogFormatter(), new FileLogWriter())
        { }

        public Logger(params ILogWriter[] logWriters) :
            this(new BasicLogFormatter(), logWriters)
        { }

        public Logger(ILogFormatter logFormatter) :
            this(logFormatter, new FileLogWriter())
        { }

        public Logger(ILogFormatter logFormatter, params ILogWriter[] logWriters)
        {
            LogFormatter = logFormatter;
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
            var log = LogFormatter.Format(logInfo);

            foreach (var writer in LogWriters)
            {
                writer.Write(log);
            }
        }

        protected IInvokerModel GetInvokerInfo()
        {
            return InvokerModelProvider.GetCurrentInvoker();
        }
    }
}