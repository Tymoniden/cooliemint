namespace WebControlCenter.Services
{
    public interface IJsonSerializerService
    {
        T Deserialize<T>(string content);

        string Serialize(object content);
    }
}