using System.Threading.Tasks;

namespace CoolieMint.WindowsClient.Services
{
    public interface IFileSystemService
    {
        Task<byte[]> ReadFile(string path);
    }
}