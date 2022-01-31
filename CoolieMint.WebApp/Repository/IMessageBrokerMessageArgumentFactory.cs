namespace WebControlCenter.Repository
{
    public interface IMessageBrokerMessageArgumentFactory
    {
        MessageBrokerMessageArgument CreateMessageBrokerMessageArgument(string topic, object payload);
        MessageBrokerMessageArgument CreateMessageBrokerMessageArgument(string topic, object payload, bool isRetained);
    }
}