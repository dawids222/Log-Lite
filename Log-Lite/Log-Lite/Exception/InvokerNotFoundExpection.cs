namespace Log_Lite.Exception
{
    public class InvokerNotFoundExpection : System.Exception
    {
        private const string MESSAGE = "Invoker can not be found. Try lowering the stack position";
        public InvokerNotFoundExpection() : base(MESSAGE)
        {

        }
    }
}
