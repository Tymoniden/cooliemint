namespace ApplicationStarter.Services
{
    public interface IJsonSerializerService
    {
        T Deserialize<T>(byte[] content);
        T Deserialize<T>(string content);
        string SerializeToString(object obj);
        byte[] Serialize(object obj);
    }
}