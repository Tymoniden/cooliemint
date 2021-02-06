using Microsoft.AspNetCore.Mvc;
using System;
using WebControlCenter.Services.Storage;

namespace WebControlCenter.Controllers
{
    public class MonitoringController : Controller
    {
        private readonly IMqttMessageCacheProvider _mqttMessageCacheProvider;

        public MonitoringController(IMqttMessageCacheProvider mqttMessageCacheProvider)
        {
            _mqttMessageCacheProvider = mqttMessageCacheProvider ?? throw new ArgumentNullException(nameof(mqttMessageCacheProvider));
        }

        public JsonResult Mqtt()
        {
            var messages = _mqttMessageCacheProvider.GetMessages();
            messages.Reverse();
            return new JsonResult(messages);
        }
    }
}
