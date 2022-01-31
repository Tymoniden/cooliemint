using System.Collections.Generic;
using System.IO;

namespace CoolieMint.WebApp.Services.FileSystem
{
    public interface IFileSystemService
    {
        void AppendAllText(string path, string text);
        string CombinePath(string path1, string path2);
        DirectoryInfo CreateDirectory(string path);
        bool DirectoryExists(string directory);
        string GetConfigurationPath();
        List<FileInfo> GetFilesInFolder(string path);
        long GetFileSize(string path);
        long GetFolderContentSize(string path);
        string GetFullPath(string path);
        string ReadFileAsString(string filename);

        string[] ReadFilesInFolder(string folder);
        bool TryCopyFolder(string sourceFolder, string destinationFolder, bool @override = true, bool recursive = true);
        void WriteAllBytes(string path, byte[] content);
        void WriteAllText(string path, string content);
        void CopyFile(string sourceFile, string destinationFile, bool @override);
        bool FileExists(string path);
    }
}