using Log_Lite.Enum;
using Log_Lite.LogFormatter;
using Log_Lite.Model;
using System;
using System.Collections.Generic;

namespace Log_Lite.LogWriter
{
    public class ConsoleLogWriter : BaseLogWriter
    {
        public ConsoleLogWriter() : this(new BasicLogFormatter(), null) { }

        public ConsoleLogWriter(ILogFormatter formatter, IEnumerable<LogLevel> allowedLogLevels = null)
            : base(formatter, allowedLogLevels) { }

        protected override void WriteWhenAllowed(LogInfo info)
        {
            var log = Formatter.Format(info);
            Console.WriteLine(log);
        }
    }
}
