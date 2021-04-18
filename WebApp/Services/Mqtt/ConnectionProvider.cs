using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebControlCenter.Services.Log;
using WebControlCenter.Services.Setting;

namespace WebControlCenter.Services.Mqtt
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly ISettingsService _settingsService;
        private readonly ILogService _logService;
        private IMqttClient _client;
        private bool _shouldBeConnected;
        private bool _isConnecting;
        private bool _publishMessages;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private Queue<MqttApplicationMessage> _outgoingMessages = new Queue<MqttApplicationMessage>();

        public ConnectionProvider(ISettingsService settingsService, ILogService logService)
        {
            _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        public bool IsConnected { get => _client.IsConnected;  }

        public int NumOutgoingMessages => _outgoingMessages.Count;

        public async Task InitializeConnection()
        {
            await InitializeClient();
            EnsureClientIsConnected();
            PublishEnqueuedMessages();
        }

        public void SendMessage(MqttApplicationMessage message)
        {
            lock (_outgoingMessages)
            {
                if(_outgoingMessages.Any(msg => msg.Topic == message.Topic && msg.Payload.SequenceEqual(message.Payload)))
                {
                    return;
                }

                _outgoingMessages.Enqueue(message);
            }
        }

        public async Task DisconnectClient()
        {
            _logService.LogInfo($"Disconnecting client through explicit call of {nameof(DisconnectClient)}");
            
            _shouldBeConnected = false;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();

            await _client.DisconnectAsync();
        }

        public void ReconnectClient()
        {
            _logService.LogInfo($"Reconnecting client through explicit call of {nameof(ReconnectClient)}");
            if (_shouldBeConnected)
            {
                _logService.LogInfo($"Should be connected was already true.");
                return;
            }

            try
            {
                if (!_cancellationTokenSource.IsCancellationRequested)
                {
                    _cancellationTokenSource.Cancel();
                    _cancellationTokenSource.Dispose();
                }
            }
            catch (ObjectDisposedException)
            {
                // do nothing
            }
            finally
            {
                _cancellationTokenSource = new CancellationTokenSource();
            }

            _logService.LogInfo($"Setting should be connected to true.");
            _shouldBeConnected = true;
        }

        public void RegisterApplicationMessageReceivedHandler(Action<MqttApplicationMessageReceivedEventArgs> messageReceivedHandler)
        {
            _client.UseApplicationMessageReceivedHandler(messageReceivedHandler);
        }

        void PublishEnqueuedMessages()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    if (_publishMessages && _client.IsConnected)
                    {
                        MqttApplicationMessage message = null;
                        lock (_outgoingMessages)
                        {
                            if (_outgoingMessages.Any())
                            {
                                message = _outgoingMessages.Dequeue();
                            }
                        }
                        
                        if(message != null)
                        {
                            await _client.PublishAsync(message);
                        }
                        
                    }

                    await Task.Delay(5);
                }
            });
        }

        void EnsureClientIsConnected()
        {
            try
            {
                Task.Run(async () =>
                {
                    while (true)
                    {
                        if (_shouldBeConnected && !_client.IsConnected)
                        {
                            _publishMessages = false;

                            if (!_isConnecting)
                            {
                                _logService.LogInfo($"[ConnectionProvider] ... client was disconnected. Trying reconnecting ...");
                                _isConnecting = true;
                                await _client.ReconnectAsync();
                            }
                        }

                        await Task.Delay(100);
                    }
                });
            }
            catch (Exception e)
            {
                _logService.LogException(e, $"[ConnectionProvider] Error while reconnecting client ({nameof(EnsureClientIsConnected)}).");
                _isConnecting = false;
            }
        }

        async Task InitializeClient()
        {
            _shouldBeConnected = true;

            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();
            _client.UseConnectedHandler(async e =>
            {
                if (_isConnecting)
                {
                    await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic("#").Build());

                    _isConnecting = false;
                    _publishMessages = true;

                    _logService.LogInfo($"[ConnectionProvider] ... connected");
                }
            });

            _client.UseDisconnectedHandler(e =>
            {
                _logService.LogInfo($"[ConnectionProvider] ... disconnected");
            });

            _isConnecting = true;

            try
            {
                _logService.LogInfo($"[ConnectionProvider] Connecting...");
                await _client.ConnectAsync(GetConnectionOptions(), _cancellationTokenSource.Token);
            }
            catch (Exception e)
            {
                _logService.LogException(e, $"[ConnectionProvider] Error while connecting client ({nameof(InitializeClient)}).");
                _isConnecting = false;
            }
        }

        IMqttClientOptions GetConnectionOptions()
        {
            var options = new MqttClientOptionsBuilder()
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
