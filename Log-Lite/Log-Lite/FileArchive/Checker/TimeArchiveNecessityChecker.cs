using LibLite.Log.Lite.Enum;
using LibLite.Log.Lite.Model.File;
using System;

namespace LibLite.Log.Lite.FileArchive.Checker
{
    public class TimeArchiveNecessityChecker : BaseArchiveNecessityChecker
    {
        private double MaxLogAge { get; }
        public TimeUnit TimeUnit { get; }

        private double FileAge => GetFileAge();

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
