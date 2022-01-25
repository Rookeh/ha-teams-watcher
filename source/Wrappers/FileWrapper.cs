using HaTeamsWatcher.Interfaces;
using System.IO;

namespace HaTeamsWatcher.Wrappers
{
    public class FileWrapper : IFile
    {
        public Stream Open(string path, FileMode mode, FileAccess access, FileShare share)
        {
            return File.Open(path, mode, access, share);
        }
    }
}