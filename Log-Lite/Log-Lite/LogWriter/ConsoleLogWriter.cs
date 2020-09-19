using Log_Lite.LogFormatter;
using Log_Lite.Model;
using System;

namespace Log_Lite.LogWriter
{
    public class ConsoleLogWriter : BaseLogWriter
    {

        public ConsoleLogWriter(ILogFormatter formatter)
            : base(formatter)
        {

        }

        public override void Write(LogInfo info)
        {
            var log = Formatter.Format(info);
            Console.WriteLine(log);
        }
    }
}
