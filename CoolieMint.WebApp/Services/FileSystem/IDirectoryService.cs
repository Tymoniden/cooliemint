namespace CoolieMint.WebApp.Services.FileSystem
{
    public interface IDirectoryService
    {
        string GetRelativeFolder(string root, params string[] relativeFolders);
    }
}