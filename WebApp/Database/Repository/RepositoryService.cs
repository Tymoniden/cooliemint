namespace WebControlCenter.Database.Repository
{
    public class RepositoryService : IRepositoryService
    {
        public ISqLiteContext GetContext()
        {
            return new SqliteContext();
        }
    }
}
