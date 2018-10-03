using System.IO;

namespace Log_Lite.FileArchive.Checker
{
    public abstract class BaseArchiveNecessityChecker : IArchiveNecessityChecker
    {
        protected FileInfo fileInfo;


        public abstract bool HaveToArchive();

        public void SetFilePath(string file)
        {
            this.fileInfo = new FileInfo(file);
        }
    }
}
