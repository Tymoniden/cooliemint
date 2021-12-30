using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolieMint.WindowsClient.Services
{
    public class FileSystemService : IFileSystemService
    {
        public async Task<byte[]> ReadFile(string path)
        {
            return await Task.FromResult(ArraySegment<byte>.Empty.Array);
        }
    }
}
