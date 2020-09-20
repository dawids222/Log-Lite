namespace Log_Lite.Service.File
{
    public interface IFileService
    {
        void Append(string path, string content);
        void Move(string sourcePath, string destinationPath);
    }
}
