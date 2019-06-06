using Log_Lite.Enum;
using Log_Lite.Model;
using System;

namespace Log_Lite.LogCreator
{
    public class LogCreator : ILogCreator
    {
        public string Create(LogInfo logInfo)
        {
            var spaces = CreateSpaces(logInfo.LogType);
            return $"{DateTime.Now}   {logInfo.LogType}   {spaces}{logInfo.InvokerInfo}  ->  {logInfo.Message}";
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
