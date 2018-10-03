using Log_Lite.Model;

namespace Log_Lite.LogCreator
{
    public interface ILogCreator
    {
        string Create(LogInfo logInfo);
    }
}
