using Log_Lite.Enum;
using Log_Lite.LogCreator;
using Log_Lite.LogWriter;
using System.Collections.Generic;

namespace Log_Lite.Logger
{
    public class Logger : ILogger
    {
        private ILogCreator logCreator;
        private List<ILogWriter> logWriters;
        protected object lockObject = new object();


        public Logger() :
            this(new LogCreator.LogCreator(), new ConsoleLogWriter())
        { }

        public Logger(params ILogWriter[] logWriters) :
            this(new LogCreator.LogCreator(), logWriters)
        { }

        public Logger(ILogCreator logCreator) :
            this(logCreator, new ConsoleLogWriter())
        { }

        public Logger(ILogCreator logCreator, params ILogWriter[] logWriters)
        {
            this.logCreator = logCreator;
            this.logWriters = new List<ILogWriter>(logWriters);
        }


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
                HandleLogging(message, type);
            }
        }

        protected void HandleLogging(object message, LogType type)
        {
            var log = logCreator.Create(type, message);

            foreach (var writer in logWriters)
            {
                writer.Write(log);
            }
        }
    }
}
