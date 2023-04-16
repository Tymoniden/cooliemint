namespace Cooliemint.Api.Server.Services;

public interface ISettingsProvider
{
    void PersistSettings();
    void ReadSettings();
    T? GetValue<T>(string key);
    void SetValue(string key, object? value);
}