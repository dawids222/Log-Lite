namespace Log_Lite.Service.File
{
    public interface IFileService
    {
        void Append(string path, string content);
    }
}
