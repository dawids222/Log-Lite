using Log_Lite.Enum;
using Log_Lite.LogCreator;
using Log_Lite.LogWriter;
using System.Threading;
using System.Threading.Tasks;

namespace Log_Lite.Logger
{
    public class AsyncLogger : Logger
    {
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

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


        protected override async void Log(object message, LogType type)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                var invokerInfo = GetInvokerInfo();
                var loggingTask = new Task(() => HandleLogging(message, type, invokerInfo));
                loggingTask.Start();
                await loggingTask;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }
}
