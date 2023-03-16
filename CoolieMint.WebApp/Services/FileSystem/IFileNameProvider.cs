namespace CoolieMint.WebApp.Services.FileSystem
{
    public interface IFileNameProvider
    {
        string GetCustomCommandFile();
        string GetCustomCommandLegacyFile();
        string GetMigrationFile();
        string GetScenesConfigFile();
    }
}