namespace WebControlCenter.Services
{
    public interface ISettingsProvider
    {
        string GetString(string settingsName);
    }
}