using System.Text.Json;
using Cooliemint.Api.Server.Services.Converter;

namespace Cooliemint.Api.Server.Services
{
    public class SettingsProvider : ISettingsProvider
    {
        private readonly string _settingsFile = Path.Combine("configuration", "settings.json");
        private readonly IJsonProviderService _jsonProviderService;
        private readonly Dictionary<string, object?> _settings = new();

        public SettingsProvider(IJsonProviderService jsonProviderService)
        {
            _jsonProviderService = jsonProviderService ?? throw new ArgumentNullException(nameof(jsonProviderService));
        }

        public T? GetValue<T>(string key)
        {
            if (!_settings.TryGetValue(key, out var value))
            {
                throw new ArgumentException($"Setting {key} was no found.", nameof(key));
            }

            var convertedValue = Convert.ChangeType(value, typeof(T));
            // TODO convert to correct value
            return (T?) value;
        }

        public void SetValue(string key, object? value)
        {
            if (!_settings.ContainsKey(key))
            {
                _settings.Add(key, value);
                return;
            }

            _settings[key] = value;
        }

        public void PersistSettings()
        {
            _jsonProviderService.SerializeToFile(_settingsFile, _settings);
        }

        public void ReadSettings()
        {
            var savedSettings = _jsonProviderService.DeserializeFile<Dictionary<string,object>>(_settingsFile);

            if (savedSettings == null || savedSettings.Count == 0)
            {
                // TODO LOG
                return;
            }

            foreach (var setting in savedSettings)
            {
                if (_settings.ContainsKey(setting.Key))
                {
                    _settings[setting.Key] = setting.Value;
                }
                else
                {
                    if (setting.Value is JsonElement jsonElement)
                    {
                        
                    }


                    _settings.Add(setting.Key, setting.Value);
                }
            }
        }
    }
}
