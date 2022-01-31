using System;
using WebControlCenter.Repository;

namespace WebControlCenter.Services.SmartDevice.Sonoff
{
    public class MqttInteractionService : IMqttInteractionService
    {
        private readonly IMessageBroker _messageBroker;
        private readonly IMessageBrokerMessageArgumentFactory _messageBrokerMessageArgumentFactory;

        public MqttInteractionService(IMessageBroker messageBroker, IMessageBrokerMessageArgumentFactory messageBrokerMessageArgumentFactory)
        {
            _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
            _messageBrokerMessageArgumentFactory = messageBrokerMessageArgumentFactory ?? throw new ArgumentNullException(nameof(messageBrokerMessageArgumentFactory));
        }

        public void SendMessage(SonoffBrokerMessage sonoffBrokerMessage)
        {
            var message = _messageBrokerMessageArgumentFactory.CreateMessageBrokerMessageArgument(
                $"{sonoffBrokerMessage.CommandPrefix}/{sonoffBrokerMessage.Identifier}/power",
                sonoffBrokerMessage.Value);

            _messageBroker.SendMessage(message);
        }
    }
}
