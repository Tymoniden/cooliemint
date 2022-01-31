using MQTTnet;
using MQTTnet.Client;
using System.Threading;
using System.Threading.Tasks;

namespace CoolieMint.WebApp.Services.Mqtt
{
    public interface IMqttClientInteractionService
    {
        Task Start(CancellationToken cancellationToken);
        void Stop();
        void RegisterClient(IMqttClient mqttClient);
        void EnqueueMessage(MqttApplicationMessage message);
        int GetOutgoingMessageCount();
    }
}