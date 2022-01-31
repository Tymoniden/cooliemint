using System.Collections.Generic;

namespace WebControlCenter.Services
{
    public interface IAdapterSettingService
    {
        void AddAdapter(string id, string adapter);
        void AddAdapters(string id, List<string> adapters);
        string CreateAdapterConfiguration();
        List<string> GetAdapters(string id);
        void Initialize();
        void ResetSettingsConfiguration();
        void LoadSettingFromUiConfig(UiConfigurationRoot configurationRoot);
    }
}