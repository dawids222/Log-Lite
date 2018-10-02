using Log_Lite.Enum;
using System;
using System.Diagnostics;

namespace Log_Lite.LogCreator
{
    public class LogCreator : ILogCreator
    {
        private uint positionInStackFrame;

        public LogCreator() : this(5)
        { }

        public LogCreator(uint positionInStackFrame)
        {
            this.positionInStackFrame = positionInStackFrame;
        }

        public string Create(LogType type, object message)
        {
            var spaces = CreateSpaces(type);
            return $"{DateTime.Now}   {type}   {spaces}{GetInvokerInfo()} -> {message}";
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

        private string GetInvokerInfo()
        {
            StackFrame frame = new StackFrame((int)positionInStackFrame);
            var method = frame.GetMethod();
            var type = method.DeclaringType.Name;
            var methodName = method.Name;
            return type + "." + methodName;
        }
    }
}
