using System;
using System.IO;

namespace Log_Lite.Model.File
{
    public class SystemFileInfo : IFileInfo
    {
        private FileInfo FileInfo { get; }

        public long Bytes => GetBytes();
        public DateTime CreationTime => FileInfo.CreationTime;

        public SystemFileInfo(string file)
        {
            FileInfo = new FileInfo(file);
        }

        private long GetBytes()
        {
            FileInfo.Refresh();
            return FileInfo.Length;
        }
    }
}
