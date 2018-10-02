using Log_Lite.Enum;
using Log_Lite.Model;
using System;

namespace Log_Lite.LogCreator
{
    public class LogCreator : ILogCreator
    {
        public string Create(LogType type, IInvokerModel invokerInfo, object message)
        {
            var spaces = CreateSpaces(type);
            return $"{DateTime.Now}   {type}   {spaces}{invokerInfo} -> {message}";
        }

        private string CreateSpaces(LogType type)
        {
            var spaces = string.Empty;

            if (type == LogType.ERROR || type == LogType.FATAL)
                spaces = "  ";
            else if (type == LogType.INFO)
                spaces = "   ";

            return spaces;
        }
    }
}
