using LibLite.Log.Lite.FileArchive.Checker;
using System.Threading.Tasks;

namespace LibLite.Log.Lite.FileArchive.Archiver
{
    public interface IFileArchiver : IArchiveNecessityChecker
    {
        void Archive();
        Task ArchiveAsync();
    }
}
