using MQTTnet;
using System;
using System.Threading.Tasks;

namespace WebControlCenter.Services.Mqtt
{
    public interface IConnectionProvider
    {
        bool IsConnected { get; }
        int NumOutgoingMessages { get; }

        Task InitializeConnection();
        void SendMessage(MqttApplicationMessage message);
        void RegisterApplicationMessageReceivedHandler(Action<MqttApplicationMessageReceivedEventArgs> messageReceivedHandler);
        Task DisconnectClient();
        void ReconnectClient();
    }
}