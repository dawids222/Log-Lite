using Log_Lite.Model;

namespace Log_Lite.LogWriter
{
    public interface ILogWriter
    {
        void Write(LogInfo info);
    }
}
