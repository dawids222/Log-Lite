using System;

namespace Log_Lite.Service.File
{
    public class SystemFileService : IFileService
    {
        public void Append(string path, string content)
        {
            System.IO.File.AppendAllText(path, content);
            System.IO.File.AppendAllText(path, Environment.NewLine);
        }
    }
}
