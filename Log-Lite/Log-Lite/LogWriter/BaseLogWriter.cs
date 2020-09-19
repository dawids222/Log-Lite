using Log_Lite.LogFormatter;
using Log_Lite.Model;

namespace Log_Lite.LogWriter
{
    public abstract class BaseLogWriter : ILogWriter
    {
        public ILogFormatter Formatter { get; }

        public BaseLogWriter(ILogFormatter formatter)
        {
            Formatter = formatter;
        }


        public abstract void Write(LogInfo info);
    }
}
