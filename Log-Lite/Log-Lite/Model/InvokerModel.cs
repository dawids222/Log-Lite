using System.Diagnostics;

namespace Log_Lite.Model
{
    public class InvokerModel : IInvokerModel
    {
        public string Class { get; private set; }

        public string Method { get; private set; }

        public void GetCurrentInvoker()
        {
            StackFrame frame = new StackFrame(4);
            var method = frame.GetMethod();
            Class = method.DeclaringType.Name;
            Method = method.Name;
        }

        public override string ToString()
        {
            return $"{Class}.{Method}";
        }
    }
}
