using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoolieMint.WebApp.Services.Mqtt
{
    public class MqttConnectionProviderService : IHostedService
    {
        private readonly IMqttConnectionProvider _mqttConnectionProvider;

        public MqttConnectionProviderService(IMqttConnectionProvider mqttConnectionProvider)
        {
            _mqttConnectionProvider = mqttConnectionProvider ?? throw new ArgumentNullException(nameof(mqttConnectionProvider));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => _mqttConnectionProvider.Start(cancellationToken));
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _mqttConnectionProvider.Stop(cancellationToken);
        }
    }
}
