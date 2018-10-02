using Log_Lite.Logger;
using Log_Lite.LogWriter;

namespace ConsoleTest
{
    public static class MyLogger
    {
        static ILogger instance;

        public static ILogger Instance
        {
            get
            {
                if (instance == null)
                {
                    var logWriter = new FileLogWriter();
                    instance = new Logger();
                }

                return instance;
            }
        }
    }
}
