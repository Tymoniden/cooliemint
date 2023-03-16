using WebControlCenter.Services;

namespace CoolieMint.WebApp.Services.FileSystem
{
    public class JsonPersistenceService : IJsonPersistenceService
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly IJsonSerializerService _jsonSerializerService;

        public JsonPersistenceService(IFileSystemService fileSystemService, IJsonSerializerService jsonSerializerService)
        {
            _fileSystemService = fileSystemService ?? throw new System.ArgumentNullException(nameof(fileSystemService));
            _jsonSerializerService = jsonSerializerService ?? throw new System.ArgumentNullException(nameof(jsonSerializerService));
        }

        public void PersistObject(object obj, params string[] path)
        {
            var fileContent = _jsonSerializerService.Serialize(obj, SerializerSettings.FileSerializer);
            var configFile = _fileSystemService.CombinePath(path);
            _fileSystemService.WriteAllText(configFile, fileContent);
        }
    }
}
