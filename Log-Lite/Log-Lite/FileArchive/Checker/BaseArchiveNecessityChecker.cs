using Log_Lite.Model.File;

namespace Log_Lite.FileArchive.Checker
{
    public abstract class BaseArchiveNecessityChecker : IArchiveNecessityChecker
    {
        protected IFileInfo FileInfo { get; }

        public abstract bool HaveToArchive();

        public BaseArchiveNecessityChecker(IFileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }
    }
}
