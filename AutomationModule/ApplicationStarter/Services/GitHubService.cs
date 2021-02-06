using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ApplicationStarter.Services
{
    public interface IGitHubService
    {
        Task<Release> QueryLatestVersion(string user, string repository, CancellationToken cancellationToken);
    }

    public class GitHubService : IGitHubService
    {
        private readonly IHttpService _httpService;
        private readonly ISerializerService _serializerService;
        private readonly IFileSystemService _fileSystemService;

        public GitHubService(IHttpService httpService, ISerializerService serializerService, IFileSystemService fileSystemService)
        {
            _httpService = httpService ?? throw new System.ArgumentNullException(nameof(httpService));
            _serializerService = serializerService ?? throw new System.ArgumentNullException(nameof(serializerService));
            _fileSystemService = fileSystemService ?? throw new System.ArgumentNullException(nameof(fileSystemService));
        }

        public async Task<Release> QueryLatestVersion(string user, string repository, CancellationToken cancellationToken)
        {
            var content = await _httpService.SendGETRequest(GetLatestAPIUri(user, repository), null, cancellationToken);

            return _serializerService.DeserializeFromString<Release>(content);
        }

        string GetLatestAPIUri(string user, string repository) 
            => $"http://api.github.com/repos/{HttpUtility.UrlEncode(user)}/{HttpUtility.UrlEncode(repository)}/releases/latest";
    }

    public class Release
    {
        public string Name { get; set; }
        public string Tag_Name { get; set; }
        public List<Asset>  Assets { get; set; }
    }

    public class Asset
    {
        public string Url { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Browser_Download_Url { get; set; }
    }

    
}
