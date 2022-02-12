using CoolieMint.WebApp.Services.Storage;
using MQTTnet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebControlCenter.CommandAdapter.Enums;
using WebControlCenter.Repository;

namespace WebControlCenter.CommandAdapter.MultiSwitch
{
    public class MultiSwitchAdapter : IMqttAdapter
    {
        IMessageBroker _messageBroker;
        private readonly IStateEntryMapper _stateEntryMapper;
        private readonly ISystemStateCache _systemStateCache;
        MultiSwitchInitializationArgument _initializationArgument;
        DateTime _lastSwitchUpdate;

        readonly List<string> _topics = new List<string>();
        readonly Dictionary<int, PowerSocketState> _switchStatus = new Dictionary<int, PowerSocketState>();
        // TODO: rename to SwitchStatus and SwitchState

        public MultiSwitchAdapter(MultiSwitchInitializationArgument initializationArgument,
            IMessageBroker messageBroker,
            IStateEntryMapper stateEntryMapper,
            ISystemStateCache systemStateCache)
        {
            _initializationArgument = initializationArgument ?? throw new ArgumentNullException(nameof(initializationArgument));
            _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
            _stateEntryMapper = stateEntryMapper ?? throw new ArgumentNullException(nameof(stateEntryMapper));
            _systemStateCache = systemStateCache ?? throw new ArgumentNullException(nameof(systemStateCache));
            Identifier = initializationArgument.Identifier;

            Setup();
        }

        public string Identifier { get; set; }

        public string Type => "Mqtt:MultiSwitchAdapter";

        public bool CanHandleMqttMessage(string topic) => _topics.Contains(topic.ToLower());

        public IAdapterStatusMessage GetStatus() => new MultiSwitchStatusMessage { TimeStamp = _lastSwitchUpdate, StateMapping = _switchStatus };

        public IAdapterStatusMessage GetStatus(DateTime timestamp)
        {
            if (_lastSwitchUpdate <= timestamp) { return null; }

            var status = new MultiSwitchStatusMessage { TimeStamp = _lastSwitchUpdate, StateMapping = _switchStatus.Select(kvp => new { index = kvp.Key, value = kvp.Value.ToString() }) };
            return status;
        }

        public void AdapterInitialize()
        {
            _messageBroker.SendMessage(new MessageBrokerMessageArgument { Topic = $"switch/{_initializationArgument.Identifier}" });
        }

        public void HandleMqttMessage(MqttApplicationMessageReceivedEventArgs eventArguments)
        {
            var message = eventArguments.ApplicationMessage;

            // handle general state message
            if (message.Topic.ToLower() == $"{_initializationArgument.TopicPrefix}switch/{_initializationArgument.Identifier}/state")
            {
                // TODO: use converter service
                var fullState = JsonConvert.DeserializeObject<int[]>(Encoding.UTF8.GetString(eventArguments.ApplicationMessage.Payload));
                for (int i = -1; i < _initializationArgument.NumSwitches-1; i++)
                {
                    var index = _initializationArgument.StartIndex + i + 1;
                    _switchStatus[index] = fullState[_initializationArgument.StartIndex + i] == 1 ? PowerSocketState.On : PowerSocketState.Off;
                    _systemStateCache.AddStateEntry(_stateEntryMapper.Map($"{this}/{index}", _switchStatus[index]));
                }
                _lastSwitchUpdate = DateTime.Now;
            } 
            else if (message.Topic.ToLower().StartsWith($"{_initializationArgument.TopicPrefix}switch/{_initializationArgument.Identifier}/state"))
            {
                var lastIndex = message.Topic.LastIndexOf("/");
                var indexFromTopic = message.Topic.Substring(lastIndex + 1, message.Topic.Length - (lastIndex + 1));
                var index = int.Parse(indexFromTopic);
                var value = int.Parse(Encoding.UTF8.GetString(eventArguments.ApplicationMessage.Payload));

                // TODO: decide if I want to verify index range
                _switchStatus[index] = value == 1 ? PowerSocketState.On : PowerSocketState.Off;
                _lastSwitchUpdate = DateTime.Now;

                _systemStateCache.AddStateEntry(_stateEntryMapper.Map($"{this}/{index}", _switchStatus[index]));
            }
        }

        public void HandleWebMessage(string payload)
        {
            var dto = JsonConvert.DeserializeObject<MultiSwitchAdapterPayload>(payload);

            _messageBroker.SendMessage(new MessageBrokerMessageArgument
            {
                Topic = $"switch/{_initializationArgument.Identifier}/{dto.Index}",
                IsRetained = false,
                Payload = dto.State == PowerSocketState.On ? 1 : 0
            });

        }

        public object GetInitializationArguments() => _initializationArgument;

        public List<string> MonitoredTopics() => _topics;

        // This method is used to prepare topics
        void Setup()
        {
            _lastSwitchUpdate = DateTime.UtcNow;

            _topics.Add($"{_initializationArgument.TopicPrefix}switch/{_initializationArgument.Identifier}");
            _topics.Add($"{_initializationArgument.TopicPrefix}switch/{_initializationArgument.Identifier}/state");
            for (int i = 0; i < _initializationArgument.NumSwitches; i++)
            {
                var runningIndex = _initializationArgument.StartIndex + i;
                _topics.Add($"{_initializationArgument.TopicPrefix}switch/{_initializationArgument.Identifier}/{runningIndex}");
                _topics.Add($"{_initializationArgument.TopicPrefix}switch/{_initializationArgument.Identifier}/state/{runningIndex}");
                _switchStatus.Add(runningIndex, PowerSocketState.Off);
            }
        }

        public override string ToString() => $"Mqtt:MultiSwitchAdapter:{_initializationArgument.Identifier}";

        public List<IControllerState> GetPossibleStates()
        {
            var states = new List<IControllerState>();

            for (int i = 0; i < _initializationArgument.NumSwitches; i++)
            {
                var runningIndex = _initializationArgument.StartIndex + i;

                states.Add(new ControllerState { State = $"{runningIndex}:off", PowerConsumption = 0.0 });
                states.Add(new ControllerState { State = $"{runningIndex}:on", PowerConsumption = 0.1 });
            }

            return states;
        }
    }
}
