using System;
using System.Threading.Tasks;

namespace LibLite.Log.Lite.Service.File
{
    public class SystemFileService : IFileService
    {
        public void Append(string path, string content)
        {
            System.IO.File.AppendAllText(path, content);
            System.IO.File.AppendAllText(path, Environment.NewLine);
        }

        public async Task AppendAsync(string path, string content)
        {
            await System.IO.File.AppendAllTextAsync(path, content);
            await System.IO.File.AppendAllTextAsync(path, Environment.NewLine);
        }

        public void Move(string sourcePath, string destinationPath)
        {
            System.IO.File.Move(sourcePath, destinationPath);
        }

        public async Task MoveAsync(string sourcePath, string destinationPath)
        {
            using var sourceStream = System.IO.File.OpenRead(sourcePath);
            using var destinationStream = System.IO.File.OpenWrite(destinationPath);
            await sourceStream.CopyToAsync(destinationStream);
            await DeleteAsync(sourcePath);
            sourceStream.Close();
            destinationStream.Close();
        }

        private Task DeleteAsync(string sourcePath)
        {
            return Task.Run(() => System.IO.File.Delete(sourcePath));
        }
    }
}
