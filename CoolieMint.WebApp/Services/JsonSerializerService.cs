using Newtonsoft.Json;
using System;

namespace WebControlCenter.Services
{
    public class JsonSerializerService : IJsonSerializerService
    {
        private readonly JsonSerializerSettings _fileSerializerSettings;
        private readonly JsonSerializerSettings _apiSerializerSettings;
        private readonly JsonSerializerSettings _slimApiSerializerSettings;

        public JsonSerializerService()
        {
            _fileSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };

            _apiSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.None
            };

            _slimApiSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None,
                Formatting = Formatting.None
            };
        }

        public T Deserialize<T>(string content) => JsonConvert.DeserializeObject<T>(content);

        public string Serialize(object content, SerializerSettings serializerSettings)
        {
            JsonSerializerSettings settings = null;
            switch (serializerSettings)
            {
                case SerializerSettings.FileSerializer:
                    settings = _fileSerializerSettings;
                    break;
                case SerializerSettings.ApiSerializer:
                    settings = _apiSerializerSettings;
                    break;
                case SerializerSettings.SlimApiSerializer:
                    settings = _slimApiSerializerSettings;
                    break;
                default:
                    throw new ArgumentException(nameof(serializerSettings));
            }
            
            return JsonConvert.SerializeObject(content, settings);
        }
    }
}