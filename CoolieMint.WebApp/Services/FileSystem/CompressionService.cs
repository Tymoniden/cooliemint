using System.IO;
using System.IO.Compression;

namespace CoolieMint.WebApp.Services.FileSystem
{
    public class CompressionService : ICompressionService
    {
        public void DecompressFile(byte[] content, string destinationDirectory)
        {
            using (MemoryStream ms = new MemoryStream(content))
            {
                DecompressFile(ms, destinationDirectory);
            }
        }

        public void DecompressFile(Stream stream, string destinationDirectory)
        {
            var archive = new ZipArchive(stream, ZipArchiveMode.Read);
            archive.ExtractToDirectory(destinationDirectory);
        }
    }
}
