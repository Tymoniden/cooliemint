namespace Cooliemint.Api.Server.Messaging.Mqtt
{
    public class MqttConnectionProvider
    {
        public void Connect() { }

        public void Disconnect() { }

        public void RegisterCallback(Func<Task> callback) { }
    }
}
