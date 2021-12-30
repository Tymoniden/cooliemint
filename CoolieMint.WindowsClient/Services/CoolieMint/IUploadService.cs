using System.Threading.Tasks;

namespace CoolieMint.WindowsClient.Services
{
    public interface IUploadService
    {
        Task Upload(byte[] content);
    }
}