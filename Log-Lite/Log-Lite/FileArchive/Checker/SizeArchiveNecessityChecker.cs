namespace Log_Lite.FileArchive.Checker
{
    public class SizeArchiveNecessityChecker : BaseArchiveNecessityChecker
    {
        private double maxLogSizeInMB;

        private double fileSizeInMB
        {
            get
            {
                fileInfo.Refresh();
                return fileInfo.Length / (1024.0f * 1024.0f);
            }
        }


        public SizeArchiveNecessityChecker(double maxLogSizeInMB)
        {
            this.maxLogSizeInMB = maxLogSizeInMB;
        }


        public override bool HaveToArchive()
        {
            return fileSizeInMB >= maxLogSizeInMB;
        }
    }
}
