using WebControlCenter.Database;

namespace WebControlCenter.Repository
{
    public class DatabaseRepository
    {
        public void InitializeDatabase()
        {
            using var dbContext = new SqliteContext();

            if (!DatabaseExists())
            {
                dbContext.Database.EnsureCreated();
            }
        }

        public bool DatabaseExists()
        {
            using var ctx = new SqliteContext();
            return ctx.Database.CanConnect();
        }
    }
}