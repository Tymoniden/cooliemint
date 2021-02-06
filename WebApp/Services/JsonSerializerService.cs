using Newtonsoft.Json;

namespace WebControlCenter.Services
{
    public class JsonSerializerService : IJsonSerializerService
    {
        public T Deserialize<T>(string content) => JsonConvert.DeserializeObject<T>(content);

        public string Serialize(object content) => JsonConvert.SerializeObject(content);
    }
}