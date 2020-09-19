using Log_Lite.Enum;
using Log_Lite.LogFormatter;
using Log_Lite.Model;
using System;
using System.Collections.Generic;

namespace Log_Lite.LogWriter
{
    public class ConsoleLogWriter : BaseLogWriter
    {
        public ConsoleLogWriter(ILogFormatter formatter, IEnumerable<LogType> allowedLogLevels = null)
            : base(formatter, allowedLogLevels) { }

        protected override void WriteWhenAllowed(LogInfo info)
        {
            var log = Formatter.Format(info);
            Console.WriteLine(log);
        }
    }
}
