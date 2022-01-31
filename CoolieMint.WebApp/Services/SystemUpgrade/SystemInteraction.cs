namespace CoolieMint.WebApp.Services.SystemUpgrade
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
