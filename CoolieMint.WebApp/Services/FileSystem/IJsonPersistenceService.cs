namespace CoolieMint.WebApp.Services.FileSystem
{
    public interface IJsonPersistenceService
    {
        void PersistObject(object obj, params string[] path);
    }
}