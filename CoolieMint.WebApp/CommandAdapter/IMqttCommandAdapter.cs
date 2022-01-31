using MQTTnet;

namespace WebControlCenter.CommandAdapter
{
    public interface IMqttCommandAdapter : ICommandAdapter
    {
        void Initialize();
        void Startup();
        void MessageReceive(MqttApplicationMessageReceivedEventArgs eventArguments);
        void RegisterAdapter(IMqttAdapter adapter);
    }
}