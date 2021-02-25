namespace WebControlCenter.Services.System
{
    public enum SystemInteraction
    {
        Shutdown,
        DisconnectMqtt,
        ConnectMqtt,
        ReloadCustomCommands,
        ReloadUiConfiguration
    }
}
