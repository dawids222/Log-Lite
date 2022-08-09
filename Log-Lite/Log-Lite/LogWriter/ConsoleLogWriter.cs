using LibLite.Log.Lite.Enum;
using LibLite.Log.Lite.LogFormatter;
using LibLite.Log.Lite.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibLite.Log.Lite.LogWriter
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

        protected override Task WriteWhenAllowedAsync(LogInfo info)
        {
            return Task.Run(() => WriteWhenAllowed(info));
        }
    }
}
