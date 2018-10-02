using Log_Lite.Enum;

namespace Log_Lite.LogCreator
{
    public interface ILogCreator
    {
        string Create(LogType type, object message);
    }
}
