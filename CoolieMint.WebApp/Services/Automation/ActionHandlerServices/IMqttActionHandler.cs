using CoolieMint.WebApp.Services.Automation.Rule.Action;

namespace CoolieMint.WebApp.Services.Automation.ActionHandlerServices
{
    public interface IMqttActionHandler
    {
        void HandleAction(MqttAction mqttAction);
    }
}