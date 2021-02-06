using System;

namespace WebControlCenter.Repository
{
    public interface IMessageBroker
    {
        void RegisterMessageCallback(Action<string, object, bool> callback);
        
        void SendMessage(MessageBrokerMessageArgument argument);
    }
}