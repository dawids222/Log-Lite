using System;

namespace LibLite.Log.Lite.Model.File
{
    public interface IFileInfo
    {
        string Path { get; }
        string DirectoryPath { get; }
        long Bytes { get; }
        DateTime CreationTime { get; }
    }
}
