using Log_Lite.Enum;
using Log_Lite.LogFormatter;
using Log_Lite.Model;
using System.Collections.Generic;
using System.Linq;

namespace Log_Lite.LogWriter
{
    public abstract class BaseLogWriter : ILogWriter
    {
        protected static readonly IEnumerable<LogLevel> AllLogLevels =
            new LogLevel[] { LogLevel.INFO, LogLevel.WARNING, LogLevel.ERROR, LogLevel.FATAL, };

        private IEnumerable<LogLevel> AllowedLogLevels { get; }
        protected ILogFormatter Formatter { get; }

        public BaseLogWriter(ILogFormatter formatter, IEnumerable<LogLevel> allowedLogLevels = null)
        {
            Formatter = formatter;
            AllowedLogLevels = allowedLogLevels ?? AllLogLevels;
        }

        public void Write(LogInfo info)
        {
            if (!AllowedLogLevels.Contains(info.Level)) { return; }
            WriteWhenAllowed(info);
        }

        protected abstract void WriteWhenAllowed(LogInfo info);
    }
}
