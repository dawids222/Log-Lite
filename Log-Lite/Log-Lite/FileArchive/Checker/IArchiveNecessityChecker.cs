namespace Log_Lite.FileArchive.Checker
{
    public interface IArchiveNecessityChecker
    {
        bool HaveToArchive();
        void SetFilePath(string file);
    }
}
