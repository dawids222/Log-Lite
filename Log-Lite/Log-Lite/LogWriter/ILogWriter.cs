using LibLite.Log.Lite.Model;
using System.Threading.Tasks;

namespace LibLite.Log.Lite.LogWriter
{
    public interface ILogWriter
    {
        void Write(LogInfo info);
        Task WriteAsync(LogInfo info);
    }
}
