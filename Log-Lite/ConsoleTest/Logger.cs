using Log_Lite.LogCreator;
using Log_Lite.Logger;

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
            var creator = new LogCreator();
            return new Logger(creator);
        }
    }
}
