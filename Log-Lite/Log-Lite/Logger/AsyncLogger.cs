﻿using Log_Lite.Enum;
using Log_Lite.LogWriter;
using Log_Lite.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Log_Lite.Logger
{
    public class AsyncLogger : Logger
    {
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);


        #region ctors
        public AsyncLogger() : base()
        { }

        public AsyncLogger(params ILogWriter[] logWriters) :
            base(logWriters)
        { }
        #endregion


        protected override async void Log(object message, LogLevel type)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                var invokerInfo = GetInvokerInfo();
                var logInfo = new LogInfo(type, invokerInfo, message);
                var loggingTask = new Task(() => HandleLogging(logInfo));
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