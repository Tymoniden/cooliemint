namespace WebControlCenter.Services.Setting
{
    public class SettingsProvider : ISettingsProvider
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly IJsonSerializerService _jsonSerializerService;
        Settings _settings = new Settings();

        public SettingsProvider(IFileSystemService fileSystemService, IJsonSerializerService jsonSerializerService)
        {
            _fileSystemService = fileSystemService ?? throw new global::System.ArgumentNullException(nameof(fileSystemService));
            _jsonSerializerService = jsonSerializerService ?? throw new global::System.ArgumentNullException(nameof(jsonSerializerService));
        }

        public Settings GetSettings() => _settings;

        public string GetString(string settingsName) => Properties.Resources.ResourceManager.GetString(settingsName);

        public void ReadSettings()
        {
            var content = _fileSystemService.ReadFileAsString("settings.json");
            _settings = _jsonSerializerService.Deserialize<Settings>(content);
        }
    }
}
