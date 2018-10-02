using System;

namespace Log_Lite.LogWriter
{
    public class ConsoleLogWriter : ILogWriter
    {
        public void Write(string log)
        {
            Console.WriteLine(log);
        }
    }
}
