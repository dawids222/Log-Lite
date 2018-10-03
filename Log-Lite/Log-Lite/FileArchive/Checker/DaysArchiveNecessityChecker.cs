using System;

namespace Log_Lite.FileArchive.Checker
{
    public class DaysArchiveNecessityChecker : BaseArchiveNecessityChecker
    {
        private uint maxLogAgeInDays;

        private int fileAgeInDays
        {
            get
            {
                fileInfo.Refresh();
                var timeFromLastWrite = DateTime.Now - fileInfo.LastWriteTime;
                return timeFromLastWrite.Days;
            }
        }


        public DaysArchiveNecessityChecker(uint maxLogAgeInDays)
        {
            this.maxLogAgeInDays = maxLogAgeInDays;
        }


        public override bool HaveToArchive()
        {
            return fileAgeInDays > 0;
        }
    }
}
