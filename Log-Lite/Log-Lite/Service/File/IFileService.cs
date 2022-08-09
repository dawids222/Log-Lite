using System.Threading.Tasks;

namespace LibLite.Log.Lite.Service.File
{
    public interface IFileService
    {
        void Append(string path, string content);
        Task AppendAsync(string path, string content);
        void Move(string sourcePath, string destinationPath);
        Task MoveAsync(string sourcePath, string destinationPath);
    }
}
