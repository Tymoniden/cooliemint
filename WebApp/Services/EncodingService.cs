using System.Text;

namespace WebControlCenter.Services
{
    public interface IEncodingService
    {
        string Decode(byte[] data);
        byte[] Encode(string data);
    }

    public class EncodingService : IEncodingService
    {
        public string Decode(byte[] data) => Encoding.UTF8.GetString(data);

        public byte[] Encode(string data) => Encoding.UTF8.GetBytes(data);
    }
}