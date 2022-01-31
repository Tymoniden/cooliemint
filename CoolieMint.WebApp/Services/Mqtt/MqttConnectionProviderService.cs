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

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _mqttConnectionProvider.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _mqttConnectionProvider.Stop(cancellationToken);
        }
    }
}
