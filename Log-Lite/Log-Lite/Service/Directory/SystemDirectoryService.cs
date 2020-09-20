namespace Log_Lite.Service.Directory
{
    public class SystemDirectoryService : IDirectoryService
    {
        public void Create(string path)
        {
            System.IO.Directory.CreateDirectory(path);
        }

        public bool Exists(string path)
        {
            return System.IO.Directory.Exists(path);
        }
    }
}
