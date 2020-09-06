using Log_Lite.Model;
using System.Diagnostics;

namespace Log_Lite.Provider.InvokerModel
{
    public class InvokerModelProvider : IInvokerModelProvider
    {
        private int StackPosition { get; } = 4;

        public IInvokerModel GetCurrentInvoker()
        {
            StackFrame frame = new StackFrame(StackPosition);
            var invokerMethod = frame.GetMethod();
            var @class = invokerMethod.DeclaringType.Name;
            var method = invokerMethod.Name;
            return new Model.InvokerModel(@class, method);
        }
    }
}
