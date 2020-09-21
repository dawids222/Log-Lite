using Log_Lite.Enum;
using Log_Lite.Model.Invoker;

namespace Log_Lite.Model
{
    public class LogInfo
    {
        public LogLevel Level { get; private set; }
        public IInvokerModel Invoker { get; private set; }
        public object Message { get; private set; }

        public LogInfo(LogLevel level, IInvokerModel invoker, object message)
        {
            Level = level;
            Invoker = invoker;
            Message = message;
        }
    }
}
