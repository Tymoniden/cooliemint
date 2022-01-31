using MQTTnet;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebControlCenter.Services.Log;

namespace CoolieMint.WebApp.Services.Mqtt
{
    public class MqttClientInteractionService : IMqttClientInteractionService
    {
        private readonly Queue<MqttApplicationMessage> _outgoingMessages = new Queue<MqttApplicationMessage>();
        private readonly ILogService _logService;
        private IMqttClient _mqttClient;
        private bool _running;
     
        public MqttClientInteractionService(ILogService logService)
        {
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        public void RegisterClient(IMqttClient mqttClient) => _mqttClient = mqttClient;

        public void EnqueueMessage(MqttApplicationMessage message)
        {
            lock (_outgoingMessages)
            {
                // only enqueue message if no identical message already exists.
                if (_outgoingMessages.Any(msg =>
                    msg.Topic == message.Topic &&
                    (msg.Payload == null && message.Payload == null || msg.Payload?.SequenceEqual(message.Payload) == true)))
                {
                    return;
                }

                _outgoingMessages.Enqueue(message);
            }
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            var wasStarted = false;
            _running = true;
            try
            {
                while (!cancellationToken.IsCancellationRequested && _running)
                {
                    try
                    {
                        if (_mqttClient?.IsConnected == true)
                        {
                            if (!wasStarted)
                            {
                                wasStarted = true;
                                _logService.LogInfo($"[MqttClientInteractionService] ... ready to send messages");
                            }

                            MqttApplicationMessage message = null;
                            lock (_outgoingMessages)
                            {
                                if (_outgoingMessages.Any())
                                {
                                    message = _outgoingMessages.Dequeue();
                                }
                            }

                            if (message != null)
                            {
                                await _mqttClient.PublishAsync(message, cancellationToken);
                            }

                            await Task.Delay(1, cancellationToken);
                        }
                        else
                        {
                            await Task.Delay(1000, cancellationToken);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        _logService.LogException(ex, "[MqttClientInteractionService] ... an error occured");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _logService.LogInfo("[MqttClientInteractionService] ... Start operation was cancelled");
            }
            finally
            {
                _logService.LogInfo("[MqttClientInteractionService] ... stopped sending messages");
            }
        }

        public void Stop()
        {
            _logService.LogInfo("[MqttClientInteractionService] ... stopping sending messages");
            _running = false;
        }

        public int GetOutgoingMessageCount() => _outgoingMessages.Count;
    }
}
