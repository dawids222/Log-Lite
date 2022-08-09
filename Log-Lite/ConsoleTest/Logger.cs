using LibLite.Log.Lite.LogFormatter;
using LibLite.Log.Lite.Logger;
using LibLite.Log.Lite.LogWriter;

namespace LibLite.Log.Lite.Playground
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
            var formatter = new BasicLogFormatter();
            var consoleWriter = new ConsoleLogWriter(formatter);

            var logger = new Logger.Logger(consoleWriter);

            return logger;
        }
    }
}
