namespace WebControlCenter.Services
{
    public interface IEncodingService
    {
        string Decode(byte[] data);
        byte[] Encode(string data);
    }
}