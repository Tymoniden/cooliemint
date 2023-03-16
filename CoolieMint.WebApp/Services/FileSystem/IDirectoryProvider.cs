namespace CoolieMint.WebApp.Services.FileSystem
{
    public interface IDirectoryProvider
    {
        string GetConfiguration();
        string GetContentRoot();
        string GetParentSystemFolder();
        string GetWebRoot();
    }
}