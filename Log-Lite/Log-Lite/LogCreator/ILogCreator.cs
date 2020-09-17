using Log_Lite.Model;

namespace Log_Lite.LogCreator
{
    public interface ILogFormatter
    {
        string Create(LogInfo logInfo);
    }
}
