namespace CoolieMint.WebApp.Services.FileSystem
{
    public interface IDirectoryProvider
    {
        string GetContentRoot();
        string GetParentSystemFolder();
        string GetWebRoot();
        string RelativeContentRoot(params string[] folders);
        string RelativeWebRoot(params string[] folders);
    }
}