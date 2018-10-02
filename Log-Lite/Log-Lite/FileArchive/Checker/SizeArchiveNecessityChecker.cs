using System.IO;

namespace Log_Lite.FileArchive.Checker
{
    public class SizeArchiveNecessityChecker : IArchiveNecessityChecker
    {
        private double maxLogSizeInMB;
        private FileInfo fileInfo;
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


        public bool HaveToArchive()
        {
            return fileSizeInMB >= maxLogSizeInMB;
        }

        public void SetFilePath(string file)
        {
            this.fileInfo = new FileInfo(file);
        }
    }
}
