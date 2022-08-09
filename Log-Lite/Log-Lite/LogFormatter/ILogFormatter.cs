using LibLite.Log.Lite.Model;

namespace LibLite.Log.Lite.LogFormatter
{
    public interface ILogFormatter
    {
        string Format(LogInfo logInfo);
    }
}
