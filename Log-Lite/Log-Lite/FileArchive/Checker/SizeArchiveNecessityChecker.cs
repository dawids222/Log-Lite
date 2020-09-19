using Log_Lite.Enum;
using Log_Lite.Model.File;

namespace Log_Lite.FileArchive.Checker
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
