using System.Text;

namespace ApplicationStarter.Services
{
    public class EncodingService : IEncodingService
    {
        public Encoding GetEncoding() => Encoding.UTF8;

        public byte[] Encode(string content) => GetEncoding().GetBytes(content);

        public string Decode(byte[] content) => GetEncoding().GetString(content);
    }
}
