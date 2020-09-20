namespace Log_Lite.Service.Directory
{
    public interface IDirectoryService
    {
        bool Exists(string path);
        void Create(string path);
    }
}
