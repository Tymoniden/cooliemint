using Cooliemint.Api.Server.Services.Storage;

namespace Cooliemint.Api.Server.Services.Converter
{
    public class JsonProviderService : IJsonProviderService
    {
        private readonly IJsonSerializeService _jsonSerializeService;
        private readonly IFileSystemService _fileSystemService;

        public JsonProviderService(IJsonSerializeService jsonSerializeService, IFileSystemService fileSystemService)
        {
            _jsonSerializeService = jsonSerializeService ?? throw new ArgumentNullException(nameof(jsonSerializeService));
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
        }

        public T? DeserializeFile<T>(string filePath) where T : class
        {
            using var stream = _fileSystemService.ReadFile(filePath);

            return _jsonSerializeService.Deserialize<T>(stream);
        }

        public void SerializeToFile(string path, object data)
        {
            using var stream = _fileSystemService.Write(path);

            _jsonSerializeService.Serialize(stream, data);
        }
    }
}
