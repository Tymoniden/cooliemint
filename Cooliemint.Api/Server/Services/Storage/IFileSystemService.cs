namespace Cooliemint.Api.Server.Services.Storage;

public interface IFileSystemService
{
    bool SetRootFolder(string path);
    bool Exists(string path);
    string GetRootedPath(string path);
    Stream ReadFile(string path);
    Stream Write(string path);
}