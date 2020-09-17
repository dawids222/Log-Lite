using Log_Lite.Exception;
using Log_Lite.Model;
using System.Diagnostics;

namespace Log_Lite.Provider.InvokerModel
{
    public class InvokerModelProvider : IInvokerModelProvider
    {
        private int StackPosition { get; }

        public InvokerModelProvider(int stackPosition = 1)
        {
            StackPosition = stackPosition;
        }

        public IInvokerModel GetCurrentInvoker()
        {
            try
            {
                var frame = new StackFrame(StackPosition);
                var invokerMethod = frame.GetMethod();
                var @class = invokerMethod.DeclaringType.Name;
                var method = invokerMethod.Name;
                return new Model.InvokerModel(@class, method);
            }
            catch (System.NullReferenceException)
            {
                throw new InvokerNotFoundExpection();
            }
        }
    }
}
