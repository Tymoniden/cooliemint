using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;

namespace CoolieMint.WebApp.Services.FileSystem
{
    public class FileSystemService : IFileSystemService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private object _syncRoot = new object();

        public FileSystemService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        }

        public void AppendAllText(string path, string text)
        {
            lock (_syncRoot)
            {
                File.AppendAllText(path, text);
            }
        }

        public string CombinePath(params string[] filesystemEntries) => Path.Combine(filesystemEntries);

        public void CopyFile(string sourceFile, string destinationFile, bool @override)
        {
            File.Copy(sourceFile, destinationFile, @override);
        }

        public DirectoryInfo CreateDirectory(string path) => Directory.CreateDirectory(path);

        public bool DirectoryExists(string directory) => Directory.Exists(directory);

        public List<FileInfo> GetFilesInFolder(string path) => new List<FileInfo>(new DirectoryInfo(path).GetFiles());

        public long GetFileSize(string path)
        {
            if (!File.Exists(path))
            {
                return 0;
            }

            return new FileInfo(path).Length;
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

        public string ReadFileAsString(string filename) => File.ReadAllText(filename);

        public string[] ReadFilesInFolder(string folder) => Directory.GetFiles(folder);

        public bool TryCopyFolder(string sourceFolder, string destinationFolder, bool @override = true, bool recursive = true)
        {
            if (sourceFolder is null)
            {
                throw new ArgumentNullException(nameof(sourceFolder));
            }

            if (destinationFolder is null)
            {
                throw new ArgumentNullException(nameof(destinationFolder));
            }

            if (!Directory.Exists(sourceFolder))
            {
                return false;
                throw new ArgumentNullException(nameof(sourceFolder), $"{nameof(sourceFolder)} does not exists.");
            }

            if (!Directory.Exists(destinationFolder))
            {
                return false;
                throw new ArgumentNullException(nameof(destinationFolder), $"{nameof(destinationFolder)} does not exists.");
            }

            foreach(var file in Directory.GetFiles(sourceFolder))
            {
                var fileInfo = new FileInfo(file);
                var destinationFileName = Path.Combine(destinationFolder, fileInfo.Name);
                if (File.Exists(destinationFileName) && !@override)
                {
                    continue;
                }

                File.Copy(file, destinationFileName, true);
            }

            foreach(var directory in Directory.GetDirectories(sourceFolder))
            {
                var directoryInfo = new DirectoryInfo(directory);
                var destinationFolderName = Path.Combine(destinationFolder, directoryInfo.Name);
                if (!Directory.Exists(destinationFolderName))
                {
                    Directory.CreateDirectory(destinationFolderName);
                }

                if (recursive)
                {
                    var sourceFolderChild = Path.Combine(sourceFolder, directoryInfo.Name);
                    if(!TryCopyFolder(sourceFolderChild, destinationFolderName, @override))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void WriteAllBytes(string path, byte[] content)
        {
            File.WriteAllBytes(path, content);
        }

        public void WriteAllText(string path, string content) => File.WriteAllText(path, content);

        public bool FileExists(string path) => File.Exists(path);
    }
}