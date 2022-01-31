using MQTTnet.Client.Options;
using System;
using WebControlCenter.Services.Setting;

namespace CoolieMint.WebApp.Services.Mqtt
{
    public class MqttSettingsProvider : IMqttSettingsProvider
    {
        private readonly MqttClientOptionsBuilder _mqttClientOptionsBuilder;
        private readonly ISettingsService _settingsService;

        public MqttSettingsProvider(MqttClientOptionsBuilder mqttClientOptionsBuilder, ISettingsService settingsService)
        {
            _mqttClientOptionsBuilder = mqttClientOptionsBuilder ?? throw ArgumentNullException(nameof(mqttClientOptionsBuilder));
            _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        }

        private Exception ArgumentNullException(string v)
        {
            throw new NotImplementedException();
        }

        public IMqttClientOptions GetConnectionOptions()
        {
            var options = _mqttClientOptionsBuilder
                .WithTcpServer(_settingsService.GetSettings().MqttServerAdress)
                .WithCleanSession();

            options.WithClientId(_settingsService.GetSettings().MqttClientId);

            if (!string.IsNullOrWhiteSpace(_settingsService.GetSettings().MqttClientUsername))
            {
                options.WithCredentials(
                    _settingsService.GetSettings().MqttClientUsername,
                    _settingsService.GetSettings().MqttClientPassword
                );
            }

            return options.Build();
        }
    }
}
