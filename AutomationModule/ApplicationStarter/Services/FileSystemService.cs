using System.IO;

namespace ApplicationStarter.Services
{
    public class FileSystemService : IFileSystemService
    {
        public bool FileExists(string filename) => File.Exists(filename);
        public bool DirectoryExists(string path) => Directory.Exists(path);
        public bool CreateDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SaveString(string content, string filename)
        {
            File.WriteAllText(filename, content);
        }

        public void Save(byte[] content, string filename)
        {
            File.WriteAllBytes(filename, content);
        }

        public string ReadAsString(string filename) => File.ReadAllText(filename);

        public byte[] Read(string filename) => File.ReadAllBytes(filename);
    }
}
