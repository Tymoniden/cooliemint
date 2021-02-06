using Microsoft.Extensions.Hosting;
using System;
using WebControlCenter.Services.Mqtt;

namespace WebControlCenter.Services.System
{
    public class SystemInteractionService : ISystemInteractionService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IConnectionProvider _connectionProvider;

        public SystemInteractionService(IHostApplicationLifetime appLifetime, IConnectionProvider connectionProvider)
        {
            _appLifetime = appLifetime ?? throw new ArgumentNullException(nameof(appLifetime));
            _connectionProvider = connectionProvider ?? throw new ArgumentNullException(nameof(connectionProvider));
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
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
