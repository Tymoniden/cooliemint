using CoolieMint.WebApp.Services.Mqtt;
using MQTTnet;
using System;
using System.Text;
using WebControlCenter.CommandAdapter;
using WebControlCenter.Services.Storage;

namespace WebControlCenter.Repository
{
    public class MqttRepository : IMqttRepository
    {
        private readonly IMqttValueProvider _valueProvider;
        private readonly IMessageBroker _messageBroker;
        private readonly IMqttCommandAdapter _commandAdapter;
        private readonly IMqttMessageCacheProvider _mqttMessageCacheProvider;
        private readonly IMqttConnectionProvider _mqttConnectionProvider;
        private readonly IMqttClientInteractionService _mqttClientInteractionService;

        public MqttRepository(
            IMqttValueProvider valueProvider, 
            IMessageBroker messageBroker, 
            IMqttCommandAdapter commandAdapter, 
            IMqttMessageCacheProvider mqttMessageCacheProvider,
            IMqttConnectionProvider mqttConnectionProvider,
            IMqttClientInteractionService mqttClientInteractionService)
        {
            _valueProvider = valueProvider ?? throw new ArgumentNullException(nameof(valueProvider));
            _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
            _commandAdapter = commandAdapter ?? throw new ArgumentNullException(nameof(commandAdapter));
            _mqttMessageCacheProvider = mqttMessageCacheProvider ?? throw new ArgumentNullException(nameof(mqttMessageCacheProvider));
            _mqttConnectionProvider = mqttConnectionProvider ?? throw new ArgumentNullException(nameof(mqttConnectionProvider));
            _mqttClientInteractionService = mqttClientInteractionService ?? throw new ArgumentNullException(nameof(mqttClientInteractionService));
        }

        public void Initialize()
        {
            // TODO remove
            _mqttConnectionProvider.RegisterApplicationMessageReceivedHandler(HandleMessage);

            _messageBroker.RegisterMessageCallback((topic, payload, isRetained) => PublishMessage(topic, payload?.ToString(), isRetained));
            _commandAdapter.Startup();
        }

        private void HandleMessage(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            _commandAdapter.MessageReceive(eventArgs);

            var contract = new MqttValueContract
            {
                Payload = eventArgs.ApplicationMessage.Payload != null ? Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload) : null,
                Topic = eventArgs.ApplicationMessage.Topic,
                TimeStamp = DateTime.Now
            };

            _mqttMessageCacheProvider.StoreIncomingMessage(contract);
            _valueProvider.SetValue(contract);
        }

        public void PublishMessage(string topic, string payload, bool retain = false)
        {
            var messageBuilder = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithRetainFlag(retain);

            if (!string.IsNullOrEmpty(payload))
            {
                messageBuilder.WithPayload(payload);
            }

            var message = messageBuilder.Build();
            _mqttMessageCacheProvider.StoreOutgoiningMessage(message);

            _mqttClientInteractionService.EnqueueMessage(message);
        }
    }
}
