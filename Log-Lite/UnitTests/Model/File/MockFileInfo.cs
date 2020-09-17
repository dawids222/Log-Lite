using Log_Lite.Model.File;
using System;

namespace UnitTests.Model.File
{
    internal class MockFileInfo : IFileInfo
    {
        public long Bytes { get; }
        public DateTime CreationTime { get; }

        public MockFileInfo(long bytes = 0, DateTime creationTime = new DateTime())
        {
            Bytes = bytes;
            CreationTime = creationTime;
        }
    }
}
