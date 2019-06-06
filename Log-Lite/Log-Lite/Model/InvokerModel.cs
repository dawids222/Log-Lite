using System.Diagnostics;

namespace Log_Lite.Model
{
    public class InvokerModel : IInvokerModel
    {
        public string Class { get; private set; }
        public string Method { get; private set; }
        private int StackPosition { get; } = 5;


        public InvokerModel()
        {
            GetCurrentInvoker();
        }


        private void GetCurrentInvoker()
        {
            StackFrame frame = new StackFrame(StackPosition);
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
