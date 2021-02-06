using System;
using System.Collections.Generic;

namespace WebControlCenter.Services
{
    public interface IUiConfigurationService
    {
        void ReadAllConfigurationFiles();

        UiConfigurationRoot GetConfiguration(string name);

        Dictionary<string, Guid> GetConfiguredUsers();
    }
}