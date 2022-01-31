using WebControlCenter.CommandAdapter;
using WebControlCenter.Services;

namespace CoolieMint.WebApp.Services.Mqtt
{
    public interface IMqttAdapterFactory
    {
        IMqttAdapter CreateMqttAdapter(MqttAdapterEntry mqttAdapterEntry);
    }
}