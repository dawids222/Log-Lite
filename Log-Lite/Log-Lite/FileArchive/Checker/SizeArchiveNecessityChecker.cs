using LibLite.Log.Lite.Enum;
using LibLite.Log.Lite.Model.File;

namespace LibLite.Log.Lite.FileArchive.Checker
{
    public class SizeArchiveNecessityChecker : BaseArchiveNecessityChecker
    {
        private double MaxSize { get; }
        private MemoryUnit MemoryUnit { get; }

        private double FileSize => FileInfo.Bytes / (double)MemoryUnit;

        public SizeArchiveNecessityChecker(IFileInfo fileInfo, double maxSize, MemoryUnit memoryUnit)
            : base(fileInfo)
        {
            MaxSize = maxSize;
            MemoryUnit = memoryUnit;
        }

        public override bool HaveToArchive()
        {
            return FileSize >= MaxSize;
        }
    }
}
