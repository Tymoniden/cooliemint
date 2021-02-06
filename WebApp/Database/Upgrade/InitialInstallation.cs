using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebControlCenter.Database.Upgrade
{
    public class InitialInstallation
    {
        public bool IsUpgradeNeeded(ISqLiteContext ctx)
        {
            if (!ctx.VersionHistory.Any())
            {
                return true;
            }

            return false;
        }

        public void Upgrade(DbContext ctx)
        {
            if (ctx is SqliteContext context)
            {
                //ctx.
            }
        }
    }
}