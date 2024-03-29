﻿using LibLite.Log.Lite.Model.File;
using System;

namespace LibLite.Log.Lite.Tests.Model.File
{
    internal class MockFileInfo : IFileInfo
    {
        public string Path { get; }
        public string DirectoryPath { get; }
        public long Bytes { get; }
        public DateTime CreationTime { get; }

        public MockFileInfo(
            string path = "",
            string directoryPath = "",
            long bytes = 0,
            DateTime creationTime = new DateTime())
        {
            Path = path;
            DirectoryPath = directoryPath;
            Bytes = bytes;
            CreationTime = creationTime;
        }
    }
}
