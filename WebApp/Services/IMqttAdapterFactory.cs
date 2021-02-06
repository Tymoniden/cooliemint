using WebControlCenter.CommandAdapter;

namespace WebControlCenter.Services
{
    public interface IMqttAdapterFactory
    {
        IMqttAdapter CreateMqttAdapter(MqttAdapterEntry mqttAdapterEntry);
    }
}