using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebControlCenter.Repository
{
    public class MessageBrokerMessageArgumentFactory : IMessageBrokerMessageArgumentFactory
    {
        public MessageBrokerMessageArgument CreateMessageBrokerMessageArgument(string topic, object payload)
        {
            return CreateMessageBrokerMessageArgument(topic, payload, false);
        }

        public MessageBrokerMessageArgument CreateMessageBrokerMessageArgument(string topic, object payload, bool isRetained)
        {
            return new MessageBrokerMessageArgument
            {
                Topic = topic,
                Payload = payload,
                IsRetained = isRetained
            };
        }
    }
}
