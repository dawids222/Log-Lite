using System.Threading.Tasks;

namespace LibLite.Log.Lite.Logger
{
    public interface ILogger
    {
        void Debug(object message);
        void Error(object message);
        void Info(object message);
        void Warning(object message);
        void Fatal(object message);

        Task DebugAsync(object message);
        Task ErrorAsync(object message);
        Task InfoAsync(object message);
        Task WarningAsync(object message);
        Task FatalAsync(object message);
    }
}
