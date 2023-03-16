using CoolieMint.WebApp.Services.Automation.Rule.Action;
using System;

namespace CoolieMint.WebApp.Services.Automation.ActionHandlerServices;

public class ActionMapperService : IActionMapperService
{
    private readonly IMqttActionHandler _mqttActionHandler;
    private readonly IValueStoreActionHandler _valueStoreActionHandler;

    public ActionMapperService(IMqttActionHandler mqttActionHandler, IValueStoreActionHandler valueStoreActionHandler)
    {
        _mqttActionHandler = mqttActionHandler ?? throw new ArgumentNullException(nameof(mqttActionHandler));
        _valueStoreActionHandler = valueStoreActionHandler ?? throw new ArgumentNullException(nameof(valueStoreActionHandler));
    }

    public void HandleAction(IAutomationAction action)
    {
        if (action is MqttAction mqttAction)
        {
            _mqttActionHandler.HandleAction(mqttAction);
            return;
        }

        if(action is ValueStoreAction valueStoreAction)
        {
            _valueStoreActionHandler.HandleAction(valueStoreAction);
            return;
        }

        throw new ArgumentException(nameof(action));
    }
}
