namespace WebControlCenter.Database.Repository
{
    public interface IRepositoryService
    {
        ISqLiteContext GetContext();
    }
}