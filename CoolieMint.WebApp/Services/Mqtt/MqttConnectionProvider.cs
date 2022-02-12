using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebControlCenter.Services.Log;

namespace CoolieMint.WebApp.Services.Mqtt
{
    public class MqttConnectionProvider : IMqttConnectionProvider
    {
        private readonly IMqttSettingsProvider _mqttSettingsProvider;
        private readonly IMqttClientInteractionService _mqttClientInteractionService;
        private readonly IMqttFactory _mqttFactory;
        private readonly ILogService _logService;
        private IMqttClient _client;
        private bool _ensurceClientIsConnected;

        public MqttConnectionProvider(
            IMqttSettingsProvider mqttSettingsProvider, 
            IMqttClientInteractionService mqttClientInteractionService, 
            IMqttFactory mqttFactory, 
            ILogService logService)
        {
            _mqttSettingsProvider = mqttSettingsProvider ?? throw new ArgumentNullException(nameof(mqttSettingsProvider));
            _mqttClientInteractionService = mqttClientInteractionService ?? throw new ArgumentNullException(nameof(mqttClientInteractionService));
            _mqttFactory = mqttFactory ?? throw new ArgumentNullException(nameof(mqttFactory));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));

            InitializeClient();
            _mqttClientInteractionService.RegisterClient(_client);
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            _logService.LogInfo($"[MqttConnectionProvider] ... connecting MqttClient");

            await _client.ConnectAsync(_mqttSettingsProvider.GetConnectionOptions(), cancellationToken);

            _ensurceClientIsConnected = true;

            _logService.LogInfo($"[MqttConnectionProvider] ... starting interaction service");

            var clientReconnectTask = Task.Run(async() => await ReconnectClient(cancellationToken), cancellationToken);
            var interactionServiceTask = Task.Run(async () => await _mqttClientInteractionService.Start(cancellationToken), cancellationToken);

            await Task.WhenAll(clientReconnectTask, interactionServiceTask);
        }

        public async Task Stop(CancellationToken cancellationToken)
        {
            _logService.LogInfo($"[MqttConnectionProvider] ... stopping interaction service");
            _mqttClientInteractionService.Stop();

            _logService.LogInfo($"[MqttConnectionProvider] ... disconnecting MqttClient");

            _ensurceClientIsConnected = false;

            await _client.DisconnectAsync();
        }

        public void RegisterApplicationMessageReceivedHandler(Action<MqttApplicationMessageReceivedEventArgs> messageReceivedHandler)
        {
            _client.UseApplicationMessageReceivedHandler(messageReceivedHandler);
        }

        public bool IsConnected() => _client.IsConnected;

        async Task ReconnectClient(CancellationToken cancellationToken)
        {
            var spin = new SpinWait();
            var cancellationTokenSource = new CancellationTokenSource();
            var timeoutInSeconds = 2;

            while (!cancellationToken.IsCancellationRequested && _ensurceClientIsConnected)
            {
                if (!_client.IsConnected)
                {
                    _logService.LogInfo($"[MqttConnectionProvider] ... reconnecting MqttClient");

                    try
                    {
                        cancellationTokenSource.Cancel();
                        cancellationTokenSource.Dispose();
                        cancellationTokenSource = new CancellationTokenSource();
                        cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(timeoutInSeconds));
                        await _client.ReconnectAsync(cancellationTokenSource.Token);
                    }
                    catch(OperationCanceledException)
                    {
                        _logService.LogInfo($"[MqttConnectionProvider] ... reconnecting timed out after {timeoutInSeconds}sec MqttClient");
                    }
                    catch (Exception ex)
                    {
                        _logService?.LogException(ex, "[MqttConnectionProvider] ... exception during reconnecting");
                    }
                }

                spin.SpinOnce();
            }
        }

        void InitializeClient()
        {
            _client = _mqttFactory.CreateMqttClient();
            _client.UseConnectedHandler(ConnectedHandler);
            _client.UseDisconnectedHandler(DisconnectedHandler);
        }

        async Task ConnectedHandler(MqttClientConnectedEventArgs args)
        {
            _logService.LogInfo($"[MqttConnectionProvider] ... MqttClient connected");

            await _client.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("#").Build());

            _logService.LogInfo($"[MqttConnectionProvider] ... MqttTopic subscribed");
        }

        Task DisconnectedHandler(MqttClientDisconnectedEventArgs args)
        {
            _logService.LogInfo($"[MqttConnectionProvider] ... MqttClient disconnected");
            return Task.CompletedTask;
        }
    }
}
