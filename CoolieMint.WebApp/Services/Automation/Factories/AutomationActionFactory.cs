using CoolieMint.WebApp.Dtos.Automation;
using CoolieMint.WebApp.Services.Automation.Rule.Action;
using System;

namespace CoolieMint.WebApp.Services.Automation.Factories
{
    public class AutomationActionFactory : IAutomationActionFactory
    {
        public IAutomationActionDto CreateAutomationActionDto(IAutomationAction automationAction)
        {
            if (automationAction is MqttAction mqttAction)
            {
                return CreateMqttAction(mqttAction);
            }

            throw new ArgumentException(nameof(automationAction));
        }

        public MqttActionDto CreateMqttAction(MqttAction mqttAction)
        {
            return new MqttActionDto
            {
                Topic = mqttAction.Topic,
                Payload = mqttAction.Payload
            };
        }
    }
}
