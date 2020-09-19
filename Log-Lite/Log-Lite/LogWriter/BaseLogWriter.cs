using Log_Lite.Enum;
using Log_Lite.LogFormatter;
using Log_Lite.Model;
using System.Collections.Generic;
using System.Linq;

namespace Log_Lite.LogWriter
{
    public abstract class BaseLogWriter : ILogWriter
    {
        protected static readonly IEnumerable<LogType> AllLogLevels =
            new LogType[] { LogType.INFO, LogType.WARNING, LogType.ERROR, LogType.FATAL, };

        private IEnumerable<LogType> AllowedLogLevels { get; }
        protected ILogFormatter Formatter { get; }

        public BaseLogWriter(ILogFormatter formatter, IEnumerable<LogType> allowedLogLevels = null)
        {
            Formatter = formatter;
            AllowedLogLevels = allowedLogLevels ?? AllLogLevels;
        }

        public void Write(LogInfo info)
        {
            if (!AllowedLogLevels.Contains(info.LogType)) { return; }
            WriteWhenAllowed(info);
        }

        protected abstract void WriteWhenAllowed(LogInfo info);
    }
}
