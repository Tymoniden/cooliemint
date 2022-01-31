using Microsoft.AspNetCore.Hosting;
using System;
using System.Diagnostics;
using System.IO;

namespace CoolieMint.WebApp.Services.FileSystem
{
    public class DirectoryProvider : IDirectoryProvider
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DirectoryProvider(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetWebRoot() => _webHostEnvironment.WebRootPath;

        public string RelativeWebRoot(params string[] folders) => GetRelativeFolder(GetWebRoot(), folders);

        public string GetContentRoot()
        {
            if (Debugger.IsAttached)
            {
                return Path.Combine(_webHostEnvironment.ContentRootPath, "bin", "Debug", "net6.0");
            }
            
            return _webHostEnvironment.ContentRootPath;
        }

        public string RelativeContentRoot(params string[] folders) => GetRelativeFolder(GetContentRoot(), folders);

        public string GetParentSystemFolder() => Path.Combine(GetContentRoot(), "..");

        string GetRelativeFolder(string root, string[] relativeFolders)
        {
            var relativeFolder = string.Join(Path.DirectorySeparatorChar, relativeFolders);
            return Path.Combine(root, relativeFolder);
        }
    }
}
