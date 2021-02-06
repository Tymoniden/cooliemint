using MQTTnet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebControlCenter.CommandAdapter.Enums;
using WebControlCenter.Database.Models;
using WebControlCenter.Database.Services;
using WebControlCenter.Repository;

namespace WebControlCenter.CommandAdapter.Sonoff
{
    public class SonoffAdapter : IMqttAdapter
    {
        readonly Dictionary<PowerSocketState, ControllerStateInformation> _stateMapping = new Dictionary<PowerSocketState, ControllerStateInformation>();
        readonly SonoffInitializationArgument _initializationArguments;
        readonly IMessageBroker _messageBroker;
        readonly IMqttAdapterDbService _mqttAdapterDbService;
        readonly IControllerStateService _controllerStateService;
        readonly IModelFactory _modelFactory;
        PowerSocketState _powerSocketState;
        DateTime _lastPowerSocketChange;
        MqttControllerState _controllerState;
        Controller _controller;

        public SonoffAdapter(
            SonoffInitializationArgument initializationArguments,
            IMessageBroker messageBroker,
            IMqttAdapterDbService mqttAdapterDbService,
            IControllerStateService controllerStateService,
            IModelFactory modelFactory)
        {
            _initializationArguments = initializationArguments;
            _messageBroker = messageBroker;
            _mqttAdapterDbService = mqttAdapterDbService ?? throw new ArgumentNullException(nameof(mqttAdapterDbService));
            _controllerStateService = controllerStateService ?? throw new ArgumentNullException(nameof(controllerStateService));
            _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
        }

        public string Identifier => _initializationArguments.Identifier;

        public string Type => "Mqtt:SonoffAdapter";

        public void HandleMqttMessage(MqttApplicationMessageReceivedEventArgs eventArguments)
        {
            var message = eventArguments.ApplicationMessage;
            if (message.Topic.ToLower() ==
                $"{_initializationArguments.TelePrefix}/{_initializationArguments.Identifier}/lwt")
            {
                var payload = Encoding.UTF8.GetString(eventArguments.ApplicationMessage.Payload).ToLower();
                _controllerState = payload == "online" ? MqttControllerState.Connected : MqttControllerState.Disconnected;
            }

            if (message.Topic.ToLower() == $"{_initializationArguments.StatPrefix}/{_initializationArguments.Identifier}/power")
            {
                var timestamp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                var payload = Encoding.UTF8.GetString(eventArguments.ApplicationMessage.Payload).ToLower();

                var newState = payload == "on" ? PowerSocketState.On : PowerSocketState.Off;
                if (newState != _powerSocketState)
                {
                    //if(newState == PowerSocketState.On)
                    //{
                    //    _controllerStateService.ReplaceState(_stateMapping[PowerSocketState.On], _stateMapping[PowerSocketState.Off]);
                    //}
                    //else
                    //{
                    //    _controllerStateService.ReplaceState(_stateMapping[PowerSocketState.Off], _stateMapping[PowerSocketState.On]);
                    //}
                }

                _powerSocketState = newState;
                _lastPowerSocketChange = timestamp;
            }
        }

        public bool CanHandleMqttMessage(string topic) => MonitoredTopics().Contains(topic.ToLower());

        public List<string> MonitoredTopics()
        {
            return new List<string>
            {
                $"{_initializationArguments.CommandPrefix}/{_initializationArguments.Identifier}/power",
                $"{_initializationArguments.StatPrefix}/{_initializationArguments.Identifier}/power",
                $"{_initializationArguments.StatPrefix}/{_initializationArguments.Identifier}/result",
                $"{_initializationArguments.TelePrefix}/{_initializationArguments.Identifier}/lwt",
                $"{_initializationArguments.TelePrefix}/{_initializationArguments.Identifier}/state"
            };
        }

        public IAdapterStatusMessage GetStatus() =>
            new PowerSocketStatus { TimeStamp = _lastPowerSocketChange, State = _powerSocketState };

        public IAdapterStatusMessage GetStatus(DateTime timestamp) =>
            _lastPowerSocketChange > timestamp
                ? new PowerSocketStatus { TimeStamp = _lastPowerSocketChange, State = _powerSocketState }
                : null;

        public void HandleWebMessage(string payload)
        {
            var dto = JsonConvert.DeserializeObject<AdapterPayload>(payload);
            if (dto == null)
            {
                return;
            }

            _messageBroker.SendMessage(new MessageBrokerMessageArgument
            {
                Topic = $"{_initializationArguments.CommandPrefix}/{_initializationArguments.Identifier}/power",
                Payload = dto.State.ToString()
            });
        }

        public object GetInitializationArguments() => _initializationArguments;

        public void AdapterInitialize()
        {
            _controller = _mqttAdapterDbService.InitializeAdapter(this);
            SetupStateMapping(_mqttAdapterDbService.GetStates(_controller));

            _messageBroker.SendMessage(new MessageBrokerMessageArgument { Topic = $"{_initializationArguments.CommandPrefix}/{_initializationArguments.Identifier}/power" });
        }

        public override string ToString() => $"Mqtt:SonoffAdapter:{_initializationArguments.Identifier}";

        public List<IControllerState> GetPossibleStates()
        {
            return new List<IControllerState>
            {
                new ControllerState{ State = $"{PowerSocketState.Off}" , PowerConsumption = 0.0 },
                new ControllerState{ State = $"{PowerSocketState.On}" , PowerConsumption = 0.1 }
            };
        }

        public void SetupStateMapping(List<ControllerStateInformation> dbStateInformations)
        {
            if (dbStateInformations?.Any() != true)
            {
                return;
            }

            foreach (var state in GetPossibleStates())
            {
                var dbState = dbStateInformations.FirstOrDefault(dbState => state.State == dbState.State);
                if (dbState != null)
                {
                    if (Enum.TryParse(typeof(PowerSocketState), state.State, true, out var parsedState))
                    {
                        _stateMapping.Add((PowerSocketState)parsedState, dbState);
                    }
                }
            }
        }
    }

    internal class AdapterPayload
    {
        public PowerSocketState State { get; set; }
    }
}