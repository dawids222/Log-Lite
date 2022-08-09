using LibLite.Log.Lite.Enum;
using LibLite.Log.Lite.LogWriter;
using LibLite.Log.Lite.Model;
using LibLite.Log.Lite.Model.Invoker;
using LibLite.Log.Lite.Provider.Invoker;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibLite.Log.Lite.Logger
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

        public void Debug(object message)
        {
            Log(message, LogLevel.DEBUG);
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

        public Task DebugAsync(object message)
        {
            return LogAsync(message, LogLevel.DEBUG);
        }

        public Task InfoAsync(object message)
        {
            return LogAsync(message, LogLevel.INFO);
        }

        public Task WarningAsync(object message)
        {
            return LogAsync(message, LogLevel.WARNING);
        }

        public Task ErrorAsync(object message)
        {
            return LogAsync(message, LogLevel.ERROR);
        }

        public Task FatalAsync(object message)
        {
            return LogAsync(message, LogLevel.FATAL);
        }

        protected virtual Task LogAsync(object message, LogLevel type)
        {
            lock (lockObject)
            {
                var invokerInfo = GetInvokerInfo();
                var logInfo = new LogInfo(type, invokerInfo, message);
                return HandleLoggingAsync(logInfo);
            }
        }

        protected Task HandleLoggingAsync(LogInfo logInfo)
        {
            var tasks = LogWriters.Select(x => x.WriteAsync(logInfo));
            return Task.WhenAll(tasks);
        }

        protected IInvokerModel GetInvokerInfo()
        {
            return InvokerModelProvider.GetCurrentInvoker();
        }
    }
}