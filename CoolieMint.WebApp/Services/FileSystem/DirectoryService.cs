using System.Collections.Generic;
using System.IO;

namespace CoolieMint.WebApp.Services.FileSystem
{
    public class DirectoryService : IDirectoryService
    {
        public string GetRelativeFolder(string root, params string[] relativeFolders)
        {
            var path = new List<string> { root };
            path.AddRange(relativeFolders);
            return Path.Combine(path.ToArray());
        }
    }
}
