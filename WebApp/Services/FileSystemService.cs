using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;

namespace WebControlCenter.Services
{
    public class FileSystemService : IFileSystemService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileSystemService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        }

        public string ReadFileAsString(string filename) => File.ReadAllText(filename);
        public string[] ReadFilesInFolder(string folder) => Directory.GetFiles(folder);

        public List<FileInfo> GetFilesInFolder(string path) => new List<FileInfo>(new DirectoryInfo(path).GetFiles());

        public void AppendAllText(string path, string text) => File.AppendAllText(path, text);

        public bool DirectoryExists(string directory) => Directory.Exists(directory);

        public DirectoryInfo CreateDirectory(string path) => Directory.CreateDirectory(path);

        public string CombinePath(string path1, string path2) => Path.Combine(path1, path2);

        public long GetFileSize(string path)
        {
            if (!File.Exists(path))
            {
                return 0;
            }

            return (new FileInfo(path)).Length;
        }

        public string GetFullPath(string path)
        {
            if (Path.IsPathRooted(path))
            {
                return path;
            }
            else
            {
                return Path.Combine(_hostingEnvironment.ContentRootPath, path);
            }
        }

        public long GetFolderContentSize(string path)
        {
            var size = 0L;
            foreach (var file in GetFilesInFolder(path))
            {
                size += file.Length;
            }

            return size;
        }
    }
}