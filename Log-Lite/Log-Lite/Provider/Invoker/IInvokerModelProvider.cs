using Log_Lite.Model.Invoker;

namespace Log_Lite.Provider.Invoker
{
    public interface IInvokerModelProvider
    {
        IInvokerModel GetCurrentInvoker();
    }
}
