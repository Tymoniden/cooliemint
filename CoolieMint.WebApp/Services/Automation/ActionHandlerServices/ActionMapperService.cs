using System;
using WebControlCenter.Automation;

namespace CoolieMint.WebApp.Services.Automation.ActionHandlerServices
{
    public class ActionMapperService : IActionMapperService
    {
        private readonly IMqttActionHandler _mqttActionHandler;

        public ActionMapperService(IMqttActionHandler mqttActionHandler)
        {
            _mqttActionHandler = mqttActionHandler ?? throw new ArgumentNullException(nameof(mqttActionHandler));
        }

        public void HandleAction(IAutomationAction action)
        {
            if (action is MqttAction mqttAction)
            {
                _mqttActionHandler.HandleAction(mqttAction);
                return;
            }

            throw new ArgumentException(nameof(action));
        }
    }
}
