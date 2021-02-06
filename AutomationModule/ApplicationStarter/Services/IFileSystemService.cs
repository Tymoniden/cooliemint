namespace ApplicationStarter.Services
{
    public interface IFileSystemService
    {
        bool FileExists(string filename);
        bool DirectoryExists(string path);
        bool CreateDirectory(string path);
        byte[] Read(string filename);
        string ReadAsString(string filename);
        void Save(byte[] content, string filename);
        void SaveString(string content, string filename);
    }
}