namespace WebControlCenter.Services
{
    public interface ISettingsService
    {
        T GetValue<T>(string settingName);
    }
}