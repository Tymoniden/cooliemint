using System;

namespace WebControlCenter.Services.Setting
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsProvider _settingsProvider;

        public SettingsService(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider ?? throw new ArgumentNullException(nameof(settingsProvider));
        }

        public Settings GetSettings() => _settingsProvider.GetSettings();
    }
}
