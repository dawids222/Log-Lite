using Log_Lite.Enum;
using Log_Lite.LogFormatter;
using Log_Lite.Model;
using System.Collections.Generic;
using System.Linq;

namespace Log_Lite.LogWriter
{
    public abstract class BaseLogWriter : ILogWriter
    {
        protected static readonly IEnumerable<LogLevel> _allLogLevels = System.Enum.GetValues(typeof(LogLevel)).Cast<LogLevel>();

        private IEnumerable<LogLevel> AllowedLogLevels { get; }
        protected ILogFormatter Formatter { get; }

        public BaseLogWriter(ILogFormatter formatter, IEnumerable<LogLevel> allowedLogLevels = null)
        {
            Formatter = formatter;
            AllowedLogLevels = allowedLogLevels ?? _allLogLevels;
        }

        public void Write(LogInfo info)
        {
            if (!AllowedLogLevels.Contains(info.Level)) { return; }
            WriteWhenAllowed(info);
        }

        protected abstract void WriteWhenAllowed(LogInfo info);
    }
}
