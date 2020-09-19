using System;

namespace Log_Lite.Model.File
{
    public interface IFileInfo
    {
        string Path { get; }
        string DirectoryPath { get; }
        long Bytes { get; }
        DateTime CreationTime { get; }
    }
}
