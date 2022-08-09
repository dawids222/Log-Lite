using System.Threading.Tasks;

namespace LibLite.Log.Lite.Service.Directory
{
    public interface IDirectoryService
    {
        bool Exists(string path);
        Task<bool> ExistsAsync(string path);
        void Create(string path);
        Task CreateAsync(string path);
    }
}
