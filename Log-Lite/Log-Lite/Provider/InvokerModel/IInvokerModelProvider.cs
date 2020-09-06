using Log_Lite.Model;

namespace Log_Lite.Provider.InvokerModel
{
    public interface IInvokerModelProvider
    {
        IInvokerModel GetCurrentInvoker();
    }
}
