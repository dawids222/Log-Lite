using Log_Lite.Model;
using System;

namespace Log_Lite.LogFormatter
{
    public class CustomLogFormatter : ILogFormatter
    {
        public Func<LogInfo, string> Formatter { get; }

        public CustomLogFormatter(Func<LogInfo, string> formatter)
        {
            Formatter = formatter;
        }

        public string Format(LogInfo logInfo)
        {
            return Formatter.Invoke(logInfo);
        }
    }
}
