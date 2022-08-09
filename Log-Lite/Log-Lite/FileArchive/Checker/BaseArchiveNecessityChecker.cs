using LibLite.Log.Lite.Model.File;

namespace LibLite.Log.Lite.FileArchive.Checker
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
