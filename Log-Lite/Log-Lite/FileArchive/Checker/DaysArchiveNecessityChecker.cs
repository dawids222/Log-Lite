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
                var currentDate = DateTime.Now.Date;
                var lastWriteDate = fileInfo.LastWriteTime.Date;
                var timeFromLastWrite = currentDate - lastWriteDate;
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
