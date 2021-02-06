using System.Threading.Tasks;

namespace ApplicationStarter.Services
{
    public interface IUpdateService
    {
        Task CheckLatestVersion();
        Task DownloadNewVersion(Release release);
    }
}