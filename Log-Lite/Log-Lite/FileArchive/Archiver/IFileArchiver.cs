namespace Log_Lite.FileArchive.Archiver
{
    public interface IFileArchiver
    {
        void Archive();
        void SetPaths(string fileToArchivePath, string directoryPath);
    }
}
