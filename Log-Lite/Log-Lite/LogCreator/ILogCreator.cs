using Log_Lite.Enum;
using Log_Lite.Model;

namespace Log_Lite.LogCreator
{
    public interface ILogCreator
    {
        string Create(LogType type, IInvokerModel InvokerInfo, object message);
    }
}
