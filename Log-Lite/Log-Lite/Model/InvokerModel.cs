using System.Diagnostics;

namespace Log_Lite.Model
{
    public class InvokerModel : IInvokerModel
    {
        public string Class { get; private set; }
        public string Method { get; private set; }


        public InvokerModel(uint positionInStack = 5)
        {
            GetCurrentInvoker(positionInStack);
        }


        private void GetCurrentInvoker(uint positionInStack)
        {
            StackFrame frame = new StackFrame((int)positionInStack);
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
