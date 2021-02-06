using ApplicationStarter.Services.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationStarter.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IConfigurationService _configurationService;
        private readonly IGitHubService _gitHubService;
        private readonly IVersionService _versionService;
        private readonly IHttpService _httpService;
        private readonly IFileSystemService _fileSystemService;
        private readonly ILoggingService _loggingService;

        public UpdateService(IConfigurationService configurationService,
                             IGitHubService gitHubService,
                             IVersionService versionService,
                             IHttpService httpService,
                             IFileSystemService fileSystemService,
                             ILoggingService loggingService)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _gitHubService = gitHubService ?? throw new ArgumentNullException(nameof(gitHubService));
            _versionService = versionService ?? throw new ArgumentNullException(nameof(versionService));
            _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        public async Task CheckLatestVersion()
        {
            _loggingService.LogInfo("checking latest version");
            var settings = _configurationService.GetSettings();
            var cancellationTokenSource = new CancellationTokenSource();

            var release = await _gitHubService.QueryLatestVersion(settings.GitHubAuthor, settings.GitHubRepository, cancellationTokenSource.Token);
            release.Tag_Name = release.Tag_Name.StartsWith("v") ? release.Tag_Name.Substring(1) : release.Tag_Name;

            if (_versionService.IsNewer(new Version(release.Tag_Name)))
            {
                await DownloadNewVersion(release);
            }
        }

        public async Task DownloadNewVersion(Release release)
        {
            var settings = _configurationService.GetSettings();

            foreach (var asset in release.Assets)
            {
                var path = Path.Combine(settings.WebAppPath, asset.Name);
                await DownloadFile(asset.Browser_Download_Url, path);
            }
        }

        //public Task UpdateWebApp()
        //{

        //}

        async Task DownloadFile(string uri, string targetFilename)
        {
            var content = await _httpService.DownloadFile(uri);
            _fileSystemService.Save(content, targetFilename);
        }
    }
}
