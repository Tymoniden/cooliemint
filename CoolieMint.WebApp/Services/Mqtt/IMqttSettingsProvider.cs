using MQTTnet.Client.Options;

namespace CoolieMint.WebApp.Services.Mqtt
{
    public interface IMqttSettingsProvider
    {
        IMqttClientOptions GetConnectionOptions();
    }
}