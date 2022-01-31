using System.IO;

namespace CoolieMint.WebApp.Services.FileSystem
{
    public interface ICompressionService
    {
        void DecompressFile(byte[] content, string destinationDirectory);
        void DecompressFile(Stream stream, string destinationDirectory);
    }
}