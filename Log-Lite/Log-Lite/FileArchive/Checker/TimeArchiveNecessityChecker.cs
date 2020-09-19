using Log_Lite.Enum;
using Log_Lite.Model.File;
using System;

namespace Log_Lite.FileArchive.Checker
{
    public class TimeArchiveNecessityChecker : BaseArchiveNecessityChecker
    {
        private double MaxLogAge { get; }

        private double FileAge => GetFileAge();

        public TimeUnit TimeUnit { get; }

        public TimeArchiveNecessityChecker(IFileInfo fileInfo, double maxLogAge, TimeUnit timeUnit)
            : base(fileInfo)
        {
            MaxLogAge = maxLogAge;
            TimeUnit = timeUnit;
        }

        public override bool HaveToArchive()
        {
            return FileAge >= MaxLogAge;
        }

        private double GetFileAge()
        {
            var fileAgeInTimeSpan = DateTime.Now - FileInfo.CreationTime;
            var fileAgeInSeconds = (int)fileAgeInTimeSpan.TotalSeconds;
            return fileAgeInSeconds / (double)TimeUnit;
        }
    }
}
