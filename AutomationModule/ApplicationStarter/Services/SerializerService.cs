using ApplicationStarter.Services.Enums;
using System;

namespace ApplicationStarter.Services
{
    public class SerializerService : ISerializerService
    {
        private readonly IJsonSerializerService _jsonSerializerService;

        public SerializerService(IJsonSerializerService jsonSerializerService)
        {
            _jsonSerializerService = jsonSerializerService ?? throw new ArgumentNullException(nameof(jsonSerializerService));
        }

        public EncodingType GetEncodingType() => EncodingType.Json;

        public byte[] Serialize(object obj, EncodingType type)
        {
            switch (type)
            {
                case EncodingType.Json:
                    return _jsonSerializerService.Serialize(obj);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

        public byte[] Serialize(object obj) => Serialize(obj, GetEncodingType());

        public T Deserialize<T>(byte[] content, EncodingType type)
        {
            switch (type)
            {
                case EncodingType.Json:
                    return _jsonSerializerService.Deserialize<T>(content);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

        public T Deserialize<T>(byte[] content) => Deserialize<T>(content, GetEncodingType());

        public T DeserializeFromString<T>(string content, EncodingType type)
        {
            switch (type)
            {
                case EncodingType.Json:
                    return _jsonSerializerService.Deserialize<T>(content);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

        public T DeserializeFromString<T>(string content) => DeserializeFromString<T>(content, GetEncodingType());
    }
}
