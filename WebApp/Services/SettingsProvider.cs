namespace WebControlCenter.Services
{
    public class SettingsProvider : ISettingsProvider
    {
        public string GetString(string settingsName) => Properties.Resources.ResourceManager.GetString(settingsName);
    }
}
