using ApplicationStarter.Services.Logging;
using System;
using System.IO;

namespace ApplicationStarter.Services
{
    public interface IVersionService
    {
        bool IsNewer(Version newVersion);

        void UpdateCurrentVersionInfomation();
    }

    public class VersionService : IVersionService
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly IConfigurationService _configurationService;
        private Version _currentVersion;

        public VersionService(IFileSystemService fileSystemService, IConfigurationService configurationService )
        {
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
        }


        public bool IsNewer(Version newVersion) => newVersion > GetCurrentVersion();

        public void UpdateCurrentVersionInfomation()
        {
            try
            {
                _currentVersion = ReadCurrentInstalledVersion();
            }
            catch
            {
                _currentVersion = new Version("0.0.0.0");
            }
        }

        Version GetCurrentVersion()
        {
            if(_currentVersion == null)
            {
                UpdateCurrentVersionInfomation();
            }

            return _currentVersion;
        }

        Version ReadCurrentInstalledVersion()
        {
            var settings = _configurationService.GetSettings();
            var buildFile = Path.Combine(settings.WebAppPath, "build.txt");

            var content = _fileSystemService.ReadAsString(buildFile);
            var lines = content.Split(Environment.NewLine);

            return new Version(lines[0]);
        }
    }
}
