using System.Threading.Tasks;

namespace CoolieMint.WebApp.Services.SystemUpgrade
{
    public interface ISystemInteractionService
    {
        Task<bool> ExcecuteAction(SystemInteraction action);
    }
}