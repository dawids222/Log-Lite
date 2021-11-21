namespace Log_Lite.Logger
{
    public interface ILogger
    {
        void Debug(object message);
        void Error(object message);
        void Info(object message);
        void Warning(object message);
        void Fatal(object message);
    }
}
