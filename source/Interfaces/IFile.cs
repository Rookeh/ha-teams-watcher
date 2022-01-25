using System.IO;

namespace HaTeamsWatcher.Interfaces
{
    public interface IFile
    {
        Stream Open(string path, FileMode mode, FileAccess access, FileShare share);
    }
}