using System.Threading.Tasks;

namespace CoolieMint.WebApp.Services.Notification.Pushover
{
    public interface IPushoverService
    {
        Task SendMessage(AppNotification appNotification);
    }
}