using CoolieMint.WebApp.Dtos.Automation;
using CoolieMint.WebApp.Services.Automation.Rule.Action;

namespace CoolieMint.WebApp.Services.Automation.Factories
{
    public interface IAutomationActionFactory
    {
        IAutomationActionDto CreateAutomationActionDto(IAutomationAction automationAction);
        MqttActionDto CreateMqttAction(MqttAction mqttAction);
    }
}