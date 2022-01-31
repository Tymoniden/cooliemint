using Microsoft.AspNetCore.Mvc;
using WebControlCenter.Services.Database;

namespace WebControlCenter.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService ?? throw new System.ArgumentNullException(nameof(notificationService));
        }

        public IActionResult Index()
        {
            ViewData["Notifications"] = _notificationService.GetSystemNotifications();

            return View();
        }

        public IActionResult Remove(long id)
        {
            _notificationService.RemoveNotification(id);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveAll()
        {
            _notificationService.RemoveAllNotifications();
            return RedirectToAction("Index");
        }
    }
}
