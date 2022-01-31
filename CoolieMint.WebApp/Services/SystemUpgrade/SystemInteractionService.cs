using CoolieMint.WebApp.Services.Mqtt;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebControlCenter.Services.CustomCommand;

namespace CoolieMint.WebApp.Services.SystemUpgrade
{
    public class SystemInteractionService : ISystemInteractionService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly ICustomCommandConfigurationService _customCommandConfigurationService;
        private readonly IMqttConnectionProvider _mqttConnectionProvider;

        public SystemInteractionService(IHostApplicationLifetime appLifetime, ICustomCommandConfigurationService customCommandConfigurationService, IMqttConnectionProvider mqttConnectionProvider)
        {
            _appLifetime = appLifetime ?? throw new ArgumentNullException(nameof(appLifetime));
            _customCommandConfigurationService = customCommandConfigurationService ?? throw new ArgumentNullException(nameof(customCommandConfigurationService));
            _mqttConnectionProvider = mqttConnectionProvider ?? throw new ArgumentNullException(nameof(mqttConnectionProvider));
        }

        public async Task<bool> ExcecuteAction(SystemInteraction action)
        {
            switch (action)
            {
                case SystemInteraction.Shutdown:
                    _appLifetime.StopApplication();
                    return false;
                case SystemInteraction.DisconnectMqtt:
                    await _mqttConnectionProvider.Stop(CancellationToken.None);
                    return true;
                case SystemInteraction.ConnectMqtt:
                    await _mqttConnectionProvider.Start(CancellationToken.None);
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
