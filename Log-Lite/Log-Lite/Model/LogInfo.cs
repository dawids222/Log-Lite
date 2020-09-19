using Log_Lite.Enum;
using Log_Lite.Model.Invoker;

namespace Log_Lite.Model
{
    public class LogInfo
    {
        public LogLevel LogLevel { get; private set; }
        public IInvokerModel InvokerInfo { get; private set; }
        public object Message { get; private set; }

        public LogInfo(LogLevel logLevel, IInvokerModel invokerInfo, object message)
        {
            LogLevel = logLevel;
            InvokerInfo = invokerInfo;
            Message = message;
        }
    }
}
