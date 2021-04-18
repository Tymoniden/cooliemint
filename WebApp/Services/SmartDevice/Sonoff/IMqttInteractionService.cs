namespace WebControlCenter.Services.SmartDevice.Sonoff
{
    public interface IMqttInteractionService
    {
        void SendMessage(SonoffBrokerMessage sonoffBrokerMessage);
    }
}