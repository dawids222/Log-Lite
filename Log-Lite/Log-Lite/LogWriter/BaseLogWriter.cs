using LibLite.Log.Lite.Enum;
using LibLite.Log.Lite.LogFormatter;
using LibLite.Log.Lite.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibLite.Log.Lite.LogWriter
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

        public Task WriteAsync(LogInfo info)
        {
            return !AllowedLogLevels.Contains(info.Level)
                ? Task.CompletedTask
                : WriteWhenAllowedAsync(info);
        }

        protected abstract void WriteWhenAllowed(LogInfo info);
        protected abstract Task WriteWhenAllowedAsync(LogInfo info);
    }
}
