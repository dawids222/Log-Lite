using System.Threading.Tasks;

namespace LibLite.Log.Lite.Service.Directory
{
    public class SystemDirectoryService : IDirectoryService
    {
        public void Create(string path) => System.IO.Directory.CreateDirectory(path);
        public Task CreateAsync(string path) => Task.Run(() => Create(path));

        public bool Exists(string path) => System.IO.Directory.Exists(path);
        public Task<bool> ExistsAsync(string path) => Task.Run(() => Exists(path));
    }
}
