using Log_Lite.Enum;
using Log_Lite.Model;
using System;

namespace Log_Lite.LogFormatter
{
    public class BasicLogFormatter : ILogFormatter
    {
        public string Format(LogInfo logInfo)
        {
            var spaces = CreateSpaces(logInfo.LogLevel);
            return $"{DateTime.Now}   {logInfo.LogLevel}   {spaces}{logInfo.InvokerInfo}  ->  {logInfo.Message}";
        }

        private string CreateSpaces(LogLevel type)
        {
            var warningTypeLength = LogLevel.WARNING.ToString().Length;
            var currentTypeLength = type.ToString().Length;
            var spacesLength = warningTypeLength - currentTypeLength;
            return "".PadLeft(spacesLength, ' ');
        }
    }
}
