namespace WebControlCenter.Services.SmartDevice.Sonoff
{
    public interface IDeviceArgumentFactory
    {
        DeviceArgument CreateDeviceArgument(string identifier);
        DeviceArgument CreateDeviceArgument(string identifier, string commandPrefix);
        DeviceArgument CreateDeviceArgument(string identifier, string commandPrefix, string telePrefix, string statPrefix);
        DeviceArgument CreateDeviceArgument(MqttAdapterEntry mqttAdapterEntry);
    }
}