using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
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

        public string GetConfiguration() => Path.Combine(GetContentRoot(), "configuration");

        public string GetWebRoot() => _webHostEnvironment.WebRootPath;

        public string GetContentRoot()
        {
            if (Debugger.IsAttached)
            {
                return Path.Combine(_webHostEnvironment.ContentRootPath, "bin", "Debug", "net6.0");
            }
            
            return _webHostEnvironment.ContentRootPath;
        }


        public string GetParentSystemFolder() => Path.Combine(GetContentRoot(), "..");
    }
}
