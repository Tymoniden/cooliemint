using System;

namespace WebControlCenter.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly IConverterService _converterService;

        public SettingsService(ISettingsProvider settingsProvider, IConverterService converterService)
        {
            _settingsProvider = settingsProvider ?? throw new ArgumentNullException(nameof(settingsProvider));
            _converterService = converterService ?? throw new ArgumentNullException(nameof(converterService));
        }

        public T GetValue<T>(string settingName)
        {
            return _converterService.ConvertValue<T>(_settingsProvider.GetString(settingName));
        }
    }
}
