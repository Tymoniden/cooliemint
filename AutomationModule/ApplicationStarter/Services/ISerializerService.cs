using ApplicationStarter.Services.Enums;

namespace ApplicationStarter.Services
{
    public interface ISerializerService
    {
        T Deserialize<T>(byte[] content, EncodingType type);
        T Deserialize<T>(byte[] content);
        T DeserializeFromString<T>(string content, EncodingType type);
        T DeserializeFromString<T>(string content);
        byte[] Serialize(object obj, EncodingType type);
        byte[] Serialize(object obj);
    }
}