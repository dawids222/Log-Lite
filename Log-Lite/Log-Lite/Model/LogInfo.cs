using Log_Lite.Enum;

namespace Log_Lite.Model
{
    public class LogInfo
    {
        public LogType LogType { get; private set; }
        public IInvokerModel InvokerInfo { get; private set; }
        public object Message { get; private set; }

        public LogInfo(LogType logType, IInvokerModel invokerInfo, object message)
        {
            LogType = logType;
            InvokerInfo = invokerInfo;
            Message = message;
        }
    }
}
