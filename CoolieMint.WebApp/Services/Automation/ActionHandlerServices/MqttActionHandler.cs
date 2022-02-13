using CoolieMint.WebApp.Services.Automation.Rule.Action;
using System;
using WebControlCenter.Repository;

namespace CoolieMint.WebApp.Services.Automation.ActionHandlerServices;

public class MqttActionHandler : IMqttActionHandler
{
    private readonly IMessageBroker _messageBroker;
    private readonly IMessageBrokerMessageArgumentFactory _messageBrokerMessageArgumentFactory;

    public MqttActionHandler(IMessageBroker messageBroker, IMessageBrokerMessageArgumentFactory messageBrokerMessageArgumentFactory)
    {
        _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
        _messageBrokerMessageArgumentFactory = messageBrokerMessageArgumentFactory ?? throw new ArgumentNullException(nameof(messageBrokerMessageArgumentFactory));
    }

    public void HandleAction(MqttAction mqttAction)
    {
        if (mqttAction == null)
        {
            throw new ArgumentException(nameof(mqttAction));
        }

        var message = _messageBrokerMessageArgumentFactory.CreateMessageBrokerMessageArgument(mqttAction.Topic, mqttAction.Payload);
        _messageBroker.SendMessage(message);
    }
}
