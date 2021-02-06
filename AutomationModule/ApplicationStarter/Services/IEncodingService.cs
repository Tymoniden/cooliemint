using System.Text;

namespace ApplicationStarter.Services
{
    public interface IEncodingService
    {
        string Decode(byte[] content);
        byte[] Encode(string content);
        Encoding GetEncoding();
    }
}