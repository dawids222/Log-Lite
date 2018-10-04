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
                    instance = CreateLogger();
                }

                return instance;
            }
        }

        private static ILogger CreateLogger()
        {
            var consoleWriter = new ConsoleLogWriter();

            var logger = new Logger();

            return logger;
        }
    }
}
