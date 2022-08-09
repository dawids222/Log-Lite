using System;
using System.IO;

namespace LibLite.Log.Lite.Model.File
{
    public class SystemFileInfo : IFileInfo
    {
        private FileInfo FileInfo { get; }

        public string Path => GetPath();
        public string DirectoryPath => GetDirectoryName();
        public long Bytes => GetBytes();
        public DateTime CreationTime => GetCreationTime();

        public SystemFileInfo(string file)
        {
            FileInfo = new FileInfo(file);
        }

        private string GetPath()
        {
            FileInfo.Refresh();
            return FileInfo.FullName;
        }

        private string GetDirectoryName()
        {
            FileInfo.Refresh();
            return FileInfo.DirectoryName;
        }

        private long GetBytes()
        {
            FileInfo.Refresh();
            return FileInfo.Exists ? FileInfo.Length : 0;
        }

        private DateTime GetCreationTime()
        {
            FileInfo.Refresh();
            return FileInfo.Exists ? FileInfo.CreationTime : DateTime.Now;
        }
    }
}
