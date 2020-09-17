using System;

namespace Log_Lite.Model.File
{
    public interface IFileInfo
    {
        long Bytes { get; }
        DateTime CreationTime { get; }
    }
}
