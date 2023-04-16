namespace Cooliemint.Api.Server.Services.Storage
{
    public class FileSystemService : IFileSystemService
    {
        private DirectoryInfo? _rootFolder;

        public bool SetRootFolder(string path)
        {
            if (Exists(path))
            {
                _rootFolder = new DirectoryInfo(path);
                return true;
            }

            return false;
        }

        public bool Exists(string path)
        {
            if (File.Exists(path) || Directory.Exists(path))
            {
                return true;
            }

            if (_rootFolder == null || string.IsNullOrWhiteSpace(_rootFolder.FullName))
            {
                return false;
            }

            // All relative paths will be checked against the root folder of the application
            // to not have to think about file system structure.
            var rootedPath = Path.Combine(_rootFolder.FullName, path);

            if (File.Exists(rootedPath) || Directory.Exists(rootedPath))
            {
                return true;
            }

            return false;
        }

        public string GetRootedPath(string path)
        {
            if (File.Exists(path))
            {
                return path;
            }

            if (Directory.Exists(path))
            {
                return path;
            }

            if (_rootFolder == null || string.IsNullOrWhiteSpace(_rootFolder.FullName))
            {
                throw new FileNotFoundException($"Could not root path: {path}");
            }

            var rootedPath = Path.Combine(_rootFolder.FullName, path); 
            if (Directory.Exists(_rootFolder.FullName))
            {
                return rootedPath;
            }
            
            throw new ArgumentException($"Could not root path: {path}");
        }

        public Stream ReadFile(string path)
        {
            if (!Exists(path))
            {
                throw new FileNotFoundException($"Could not read file: {path}");
            }

            var rootedPath = GetRootedPath(path);

            return File.OpenRead(rootedPath);
        }

        public Stream Write(string path)
        {
            var rootedPath = GetRootedPath(path);
            if (!Exists(rootedPath))
            {
                var fileInfo = new FileInfo(rootedPath);
                if (fileInfo?.Directory?.Exists != true && !string.IsNullOrWhiteSpace(fileInfo?.DirectoryName))
                {
                    Directory.CreateDirectory(fileInfo.DirectoryName);
                }

                return File.Create(rootedPath);
            }

            return File.OpenWrite(rootedPath);
        }
    }
}
