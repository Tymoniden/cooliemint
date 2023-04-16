namespace Cooliemint.Api.Server.Services.Converter;

public interface IJsonProviderService
{
    T? DeserializeFile<T>(string filePath) where T : class;
    void SerializeToFile(string path, object data);
}