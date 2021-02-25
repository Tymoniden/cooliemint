namespace WebControlCenter.Services.SmartDevice.Sonoff
{
    public interface IBrokerMessageFactory
    {
        SonoffBrokerMessage CreateBrokerMessage(DeviceArgument deviceArgument, OperationType operationType);
    }
}