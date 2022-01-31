using WebControlCenter.Services.Setting;

namespace WebControlCenter.Services
{
    public interface ISettingsProvider
    {
        Settings GetSettings();
        string GetString(string settingsName);
        void ReadSettings();
    }
}