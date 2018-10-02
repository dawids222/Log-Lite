using Log_Lite.Enum;
using Log_Lite.LogCreator;
using Log_Lite.LogWriter;
using System.Threading.Tasks;

namespace Log_Lite.Logger
{
    public class AsyncLogger : Logger
    {
        public AsyncLogger() : base()
        { }

        public AsyncLogger(params ILogWriter[] logWriters) :
            base(logWriters)
        { }

        public AsyncLogger(ILogCreator logCreator) :
            base(logCreator)
        { }

        public AsyncLogger(ILogCreator logCreator, params ILogWriter[] logWriters) :
            base(logCreator, logWriters)
        { }


        protected override void Log(object message, LogType type)
        {
            lock (lockObject)
            {
                var invokerInfo = GetInvokerInfo();

                var loggingTask = new Task(() => HandleLogging(message, type, invokerInfo));
                loggingTask.Start();
                loggingTask.Wait();
            }
        }
    }
}
