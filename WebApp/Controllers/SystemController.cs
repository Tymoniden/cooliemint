using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using WebControlCenter.Services.Mqtt;
using WebControlCenter.Services.System;

namespace WebControlCenter.Controllers
{
    public class SystemController : Controller
    {
        private readonly ISystemInteractionService _systemInteractionService;
        private readonly IConnectionProvider _connectionProvider;

        public SystemController(ISystemInteractionService systemInteractionService, IConnectionProvider connectionProvider)
        {
            _systemInteractionService = systemInteractionService ?? throw new ArgumentNullException(nameof(systemInteractionService));
            _connectionProvider = connectionProvider ?? throw new ArgumentNullException(nameof(connectionProvider));
        }

        public IActionResult Information()
        {
            ViewData["Version"] = GetType().Assembly.GetName().Version.ToString();
            ViewData["MqttConnected"] = _connectionProvider.IsConnected.ToString();

            ViewData["numMqttOut"] = _connectionProvider.NumOutgoingMessages;
            return View();
        }

        public IActionResult Interaction()
        {
            return View();
        }

        public IActionResult Action(string actionName)
        {
            if(Enum.TryParse<SystemInteraction>(actionName, true, out var action))
            {
                if (!_systemInteractionService.ExcecuteAction(action))
                {
                    return Content("shutting down ...");
                }
            }

            return RedirectToAction("Interaction");
        }
    }
}
