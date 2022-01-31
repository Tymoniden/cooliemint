using System;
using System.Collections.Generic;

namespace WebControlCenter.Repository
{
    public class MessageBroker : IMessageBroker
    {
        private readonly List<Action<string, object, bool>> _actions = new List<Action<string, object, bool>>();

        public void RegisterMessageCallback(Action<string, object, bool> callback)
        {
            _actions.Add(callback);
        }

        public void SendMessage(MessageBrokerMessageArgument argument)
        {
            foreach (var action in _actions)
            {
                action(argument.Topic, argument.Payload, argument.IsRetained);
            }
        }
    }
}
