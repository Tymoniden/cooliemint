using Microsoft.Extensions.Hosting;
using System;
using WebControlCenter.Services.CustomCommand;
using WebControlCenter.Services.Mqtt;

namespace WebControlCenter.Services.System
{
    public class SystemInteractionService : ISystemInteractionService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IConnectionProvider _connectionProvider;
        private readonly ICustomCommandConfigurationService _customCommandConfigurationService;

        public SystemInteractionService(IHostApplicationLifetime appLifetime, IConnectionProvider connectionProvider, ICustomCommandConfigurationService customCommandConfigurationService)
        {
            _appLifetime = appLifetime ?? throw new ArgumentNullException(nameof(appLifetime));
            _connectionProvider = connectionProvider ?? throw new ArgumentNullException(nameof(connectionProvider));
            _customCommandConfigurationService = customCommandConfigurationService ?? throw new ArgumentNullException(nameof(customCommandConfigurationService));
        }

        public bool ExcecuteAction(SystemInteraction action)
        {
            switch (action)
            {
                case SystemInteraction.Shutdown:
                    _appLifetime.StopApplication();
                    return false;
                case SystemInteraction.DisconnectMqtt:
                    _connectionProvider.DisconnectClient();
                    return true;
                case SystemInteraction.ConnectMqtt:
                    _connectionProvider.ReconnectClient();
                    return true;
                case SystemInteraction.ReloadCustomCommands:
                    _customCommandConfigurationService.ReloadConfiguration();
                    return true;
                case SystemInteraction.ReloadUiConfiguration:
                    return false;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
