using Log_Lite.Enum;
using Log_Lite.Model.File;
using System;

namespace Log_Lite.FileArchive.Checker
{
    public class DaysArchiveNecessityChecker : BaseArchiveNecessityChecker
    {
        private uint MaxLogAge { get; }

        private int FileAge => GetFileAge();

        public TimeUnit TimeUnit { get; }

        public DaysArchiveNecessityChecker(IFileInfo fileInfo, uint maxLogAge, TimeUnit timeUnit)
            : base(fileInfo)
        {
            MaxLogAge = maxLogAge;
            TimeUnit = timeUnit;
        }

        public override bool HaveToArchive()
        {
            return FileAge >= MaxLogAge;
        }

        private int GetFileAge()
        {
            var fileAgeInTimeSpan = DateTime.Now - FileInfo.CreationTime;
            var fileAgeInSeconds = (int)fileAgeInTimeSpan.TotalSeconds;
            return fileAgeInSeconds / (int)TimeUnit;
        }
    }
}
