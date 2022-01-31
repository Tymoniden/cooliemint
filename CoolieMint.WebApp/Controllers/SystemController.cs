using CoolieMint.WebApp.Services.Mqtt;
using CoolieMint.WebApp.Services.SystemUpgrade;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebControlCenter.Controllers
{
    public class SystemController : Controller
    {
        private readonly ISystemInteractionService _systemInteractionService;
        private readonly IMqttClientInteractionService _mqttClientInteractionService;
        private readonly IMqttConnectionProvider _mqttConnectionProvider;

        public SystemController(ISystemInteractionService systemInteractionService, IMqttClientInteractionService mqttClientInteractionService, IMqttConnectionProvider mqttConnectionProvider)
        {
            _systemInteractionService = systemInteractionService ?? throw new ArgumentNullException(nameof(systemInteractionService));
            _mqttClientInteractionService = mqttClientInteractionService ?? throw new ArgumentNullException(nameof(mqttClientInteractionService));
            _mqttConnectionProvider = mqttConnectionProvider ?? throw new ArgumentNullException(nameof(mqttConnectionProvider));
        }

        public IActionResult Information()
        {
            ViewData["Version"] = GetType().Assembly.GetName().Version.ToString();
            ViewData["MqttConnected"] = _mqttConnectionProvider.IsConnected().ToString();

            ViewData["numMqttOut"] = _mqttClientInteractionService.GetOutgoingMessageCount().ToString();
            return View();
        }

        public IActionResult Interaction()
        {
            return View();
        }

        public async Task<IActionResult> Action(string actionName)
        {
            if(Enum.TryParse<SystemInteraction>(actionName, true, out var action))
            {
                if (!await _systemInteractionService.ExcecuteAction(action))
                {
                    return Content("shutting down ...");
                }
            }

            return RedirectToAction("Interaction");
        }
    }
}
