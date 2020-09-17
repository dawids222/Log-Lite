using Log_Lite.Model;

namespace Log_Lite.LogFormatter
{
    public interface ILogFormatter
    {
        string Format(LogInfo logInfo);
    }
}
