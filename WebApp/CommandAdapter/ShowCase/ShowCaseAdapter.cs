using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MQTTnet;
using WebControlCenter.CommandAdapter.Enums;
using WebControlCenter.Repository;
using Newtonsoft.Json;

namespace WebControlCenter.CommandAdapter.ShowCase
{
    public class ShowCaseAdapter : IMqttAdapter
    {
        readonly ShowCaseInitializationArgument _initializationArgument;
        readonly IMessageBroker _messageBroker;
        readonly List<string> _topics = new List<string>();
        readonly Dictionary<int, LightState> _lightStatus = new Dictionary<int, LightState>();
        DateTime _lightStateChange;

        public ShowCaseAdapter(ShowCaseInitializationArgument initializationArgument, IMessageBroker messageBroker)
        {
            _initializationArgument = initializationArgument;
            _messageBroker = messageBroker;

            Setup();
        }

        void Setup()
        {
            _lightStateChange = DateTime.UtcNow;
            //_topics.Add($"{_initializationArgument.TopicPrefix}/ShowCase/{_initializationArgument.Identifier}");
            _topics.Add($"{_initializationArgument.TopicPrefix}{_initializationArgument.Identifier}");
            for (int i = _initializationArgument.LightStartIndex; i < _initializationArgument.LightStartIndex + _initializationArgument.Columns * _initializationArgument.Rows; i++)
            {
                _topics.Add($"{_initializationArgument.TopicPrefix}light/{i}");
                //_topics.Add($"{_initializationArgument.TopicPrefix}/ShowCase/{_initializationArgument.Identifier}/Light/{i}");
                _lightStatus.Add(i, LightState.Off);
            }
        }

        public string Identifier => _initializationArgument.Identifier;

        public string Type => "Mqtt:ShowCaseAdapter";

        public bool CanHandleMqttMessage(string topic) => _topics.Contains(topic.ToLower());

        public IAdapterStatusMessage GetStatus() => new ShowCaseStatusMessage { TimeStamp = _lightStateChange, StateMapping = _lightStatus };

        public IAdapterStatusMessage GetStatus(DateTime timestamp)
        {
            if (_lightStateChange <= timestamp){ return null; }

            var status = new ShowCaseStatusMessage { TimeStamp = _lightStateChange, StateMapping = _lightStatus.Select(kvp => new { index = kvp.Key, value = kvp.Value.ToString() }) };
            return status;
        }

        public void AdapterInitialize()
        {
            _messageBroker.SendMessage(new MessageBrokerMessageArgument { Topic = $"{_initializationArgument.TopicPrefix}showcase" });
        }

        public void HandleMqttMessage(MqttApplicationMessageReceivedEventArgs eventArguments)
        {
            var message = eventArguments.ApplicationMessage;
            //if (message.Topic.ToLower().StartsWith($"{_initializationArgument.TopicPrefix}/showcase/{_initializationArgument.Identifier}/light/"))
            if (message.Topic.ToLower().StartsWith($"{_initializationArgument.TopicPrefix}light/"))
            {
                var lastIndex = message.Topic.LastIndexOf("/");
                var indexFromTopic = message.Topic.Substring(lastIndex + 1, message.Topic.Length - (lastIndex + 1));
                var index = int.Parse(indexFromTopic);
                var value = int.Parse(Encoding.UTF8.GetString(eventArguments.ApplicationMessage.Payload));
                _lightStatus[index] = value == 1 ? LightState.On : LightState.Off;
                _lightStateChange = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            }

            if (message.Topic.ToLower() == $"{_initializationArgument.TopicPrefix}showcase/{_initializationArgument.Identifier}")
            {

            }
        }

        public List<string> MonitoredTopics() => _topics;

        public void HandleWebMessage(string payload)
        {
            var dto = JsonConvert.DeserializeObject<AdapterPayload>(payload);
            
            _messageBroker.SendMessage(new MessageBrokerMessageArgument
            {
                Topic = $"light/{_initializationArgument.LightStartIndex + dto.Index -1}",
                IsRetained = false,
                Payload = dto.State == LightState.On ? 1 : 0
            });
        }

        public object GetInitializationArguments() => _initializationArgument;

        public override string ToString() => $"{Type}:{Identifier}";

        public static ShowCaseAdapter CreateAdapter(ShowCaseInitializationArgument initializationArgument, IMessageBroker messageBroker) =>
            new ShowCaseAdapter(initializationArgument, messageBroker);

        public List<IControllerState> GetPossibleStates()
        {
            return new List<IControllerState>
            {
                new ControllerState
                {
                    State = "Off",
                    PowerConsumption = 0.0
                },
                new ControllerState
                {
                    State = "1",
                    PowerConsumption = 0.1
                },
                new ControllerState
                {
                    State = "2",
                    PowerConsumption = 0.2
                },
                new ControllerState
                {
                    State = "3",
                    PowerConsumption = 0.3
                },
                new ControllerState
                {
                    State = "4",
                    PowerConsumption = 0.4
                },
                new ControllerState
                {
                    State = "5",
                    PowerConsumption = 0.5
                },
                new ControllerState
                {
                    State = "6",
                    PowerConsumption = 0.6
                },
                new ControllerState
                {
                    State = "7",
                    PowerConsumption = 0.7
                },
                new ControllerState
                {
                    State = "8",
                    PowerConsumption = 0.8
                },
            };
        }
    }
}
