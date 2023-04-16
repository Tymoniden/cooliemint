using System.Text.Json;

namespace Cooliemint.Api.Server.Services.Converter
{
    public class JsonSerializeService : IJsonSerializeService
    {
        private readonly JsonSerializerOptions _defaultOptions;

        public JsonSerializeService()
        {
            _defaultOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Type
            };
        }

        public T? Deserialize<T>(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            
            return JsonSerializer.Deserialize<T>(stream, _defaultOptions);
        }

        public void Serialize(Stream stream, object data)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            JsonSerializer.Serialize(stream, data, _defaultOptions);
        }
    }
}
