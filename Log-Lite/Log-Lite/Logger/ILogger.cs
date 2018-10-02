namespace Log_Lite.Logger
{
    public interface ILogger
    {
        void Error(object message);
        void Info(object message);
        void Warning(object message);
        void Fatal(object message);
    }
}
