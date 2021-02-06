using System.Collections.Generic;

namespace ApplicationStarter.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private const string _fileName = "configuration.json";
        private readonly IFileSystemService _fileSystemService;
        private readonly ISerializerService _serializerService;

        private Settings _settings;

        public ConfigurationService(IFileSystemService fileSystemService, ISerializerService serializerService)
        {
            _fileSystemService = fileSystemService ?? throw new System.ArgumentNullException(nameof(fileSystemService));
            _serializerService = serializerService ?? throw new System.ArgumentNullException(nameof(serializerService));
        }

        public Settings GetSettings()
        {
            if(_settings == null)
            {
                _settings = ReadSettings();
            }

            return _settings;
        }

        Settings ReadSettings()
        {
            var content = _fileSystemService.Read(_fileName);
            return _serializerService.Deserialize<Settings>(content);
        }
    }

    public class Settings
    {
        public List<string> ObservedApplications { get; set; }

        public string WebAppPath { get; set; }

        public string GitHubAuthor { get; set; }

        public string GitHubRepository { get; set; }

        public string LogPath { get; set; }
    }
}
