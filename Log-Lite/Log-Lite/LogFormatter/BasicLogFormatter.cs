using Log_Lite.Enum;
using Log_Lite.Model;
using System;

namespace Log_Lite.LogFormatter
{
    public class BasicLogFormatter : ILogFormatter
    {
        public string Format(LogInfo logInfo)
        {
            var spaces = CreateSpaces(logInfo.Level);
            return $"{DateTime.Now}   {logInfo.Level}   {spaces}{logInfo.Invoker}  ->  {logInfo.Message}";
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
