using Newtonsoft.Json;
using System.IO;

namespace ApplicationStarter.Services
{
    public class JsonSerializerService : IJsonSerializerService
    {
        private readonly IEncodingService _encodingService;

        public JsonSerializerService(IEncodingService encodingService)
        {
            _encodingService = encodingService ?? throw new System.ArgumentNullException(nameof(encodingService));
        }

        public string SerializeToString(object obj) => JsonConvert.SerializeObject(obj);

        public byte[] Serialize(object obj) => _encodingService.Encode(JsonConvert.SerializeObject(obj));

        public T Deserialize<T>(byte[] content)
        {
            using (var stream = new MemoryStream(content))
            using (var reader = new StreamReader(stream, _encodingService.GetEncoding()))
                return (T)JsonSerializer.Create().Deserialize(reader, typeof(T));
        }

        public T Deserialize<T>(string content) => JsonConvert.DeserializeObject<T>(content);
    }
}
