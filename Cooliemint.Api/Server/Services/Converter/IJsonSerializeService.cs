namespace Cooliemint.Api.Server.Services.Converter;

public interface IJsonSerializeService
{
    T? Deserialize<T>(Stream stream);
    void Serialize(Stream stream, object data);
}