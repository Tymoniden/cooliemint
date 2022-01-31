using System.Threading.Tasks;

namespace CoolieMint.WebApp.Services.SystemUpgrade.Migration
{
    public interface IConfigurationMigrationService
    {
        Task<bool> Migrate();
    }
}