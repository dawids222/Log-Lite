using Log_Lite.FileArchive.Checker;

namespace Log_Lite.FileArchive.Archiver
{
    public interface IFileArchiver : IArchiveNecessityChecker
    {
        void Archive();
    }
}
