using MQTTnet;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoolieMint.WebApp.Services.Mqtt
{
    public interface IMqttConnectionProvider
    {
        bool IsConnected();
        void RegisterApplicationMessageReceivedHandler(Action<MqttApplicationMessageReceivedEventArgs> messageReceivedHandler);
        Task Start(CancellationToken cancellationToken);
        Task Stop(CancellationToken cancellationToken);
    }
}