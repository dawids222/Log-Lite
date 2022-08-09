using LibLite.Log.Lite.Model.Invoker;

namespace LibLite.Log.Lite.Provider.Invoker
{
    public interface IInvokerModelProvider
    {
        IInvokerModel GetCurrentInvoker();
    }
}
