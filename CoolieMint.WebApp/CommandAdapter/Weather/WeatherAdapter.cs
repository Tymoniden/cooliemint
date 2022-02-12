using System;
using System.Collections.Generic;
using CoolieMint.WebApp.Services;
using CoolieMint.WebApp.Services.Automation;
using CoolieMint.WebApp.Services.Storage;
using MQTTnet;
using WebControlCenter.CommandAdapter.Enums;
using WebControlCenter.CommandAdapter.Temperature;
using WebControlCenter.Repository;
using WebControlCenter.Services;

namespace WebControlCenter.CommandAdapter.Weather
{
    public class WeatherAdapter : IMqttAdapter
    {
        private readonly IJsonSerializerService _jsonSerializerService;
        private readonly IEncodingService _encodingService;
        private readonly IStateEntryMapper _stateEntryMapper;
        private readonly ISystemStateCache _systemStateCache;
        private WeatherStatus _status;
        private WeatherAdapterInitializationArgument _weatherAdapterInitializationArgument;

        public WeatherAdapter(WeatherAdapterInitializationArgument weatherAdapterInitializationArgument,
            IJsonSerializerService jsonSerializerService,
            IEncodingService encodingService,
            IStateEntryMapper stateEntryMapper,
            ISystemStateCache systemStateCache)
        {
            _jsonSerializerService = jsonSerializerService ?? throw new ArgumentNullException(nameof(jsonSerializerService));
            _encodingService = encodingService ?? throw new ArgumentNullException(nameof(encodingService));
            _stateEntryMapper = stateEntryMapper ?? throw new ArgumentNullException(nameof(stateEntryMapper));
            _systemStateCache = systemStateCache ?? throw new ArgumentNullException(nameof(systemStateCache));
            _weatherAdapterInitializationArgument = weatherAdapterInitializationArgument;
        }
        
        public string Identifier => _weatherAdapterInitializationArgument.Identifier;

        public string Type => "Mqtt:WeatherAdapter";

        public void AdapterInitialize()
        {
        }

        public List<string> MonitoredTopics() => new List<string> { $"{_weatherAdapterInitializationArgument.TopicPrefix}weather/{_weatherAdapterInitializationArgument.Identifier}" };

        public bool CanHandleMqttMessage(string topic) => MonitoredTopics().Contains(topic);

        public void HandleMqttMessage(MqttApplicationMessageReceivedEventArgs eventArguments)
        {
            var message = eventArguments.ApplicationMessage;
            if (message.Topic.ToLower() ==
                $"{_weatherAdapterInitializationArgument.TopicPrefix}weather/{_weatherAdapterInitializationArgument.Identifier}")
            {
                var decodedString = _encodingService.Decode(eventArguments.ApplicationMessage.Payload);
                try
                {
                    var payload = _jsonSerializerService.Deserialize<WeatherAdapterPayload>(decodedString);
                    if (payload != null)
                    {
                        _status = new WeatherStatus
                        {
                            Temperature = payload.Temperature,
                            Humidity = Convert.ToInt32(payload.Humidity),
                            Pressure = Convert.ToInt32(payload.Pressure),
                            TimeStamp = DateTime.Now
                        };

                        _systemStateCache.AddStateEntry(_stateEntryMapper.Map(ToString(), _status));
                    }
                }
                catch
                {
                }
            }
        }

        public IAdapterStatusMessage GetStatus() => _status;

        public IAdapterStatusMessage GetStatus(DateTime timestamp) => _status?.TimeStamp != null && _status.TimeStamp > timestamp ? _status: null;

        public void HandleWebMessage(string payload)
        {
            throw new NotImplementedException();
        }

        public object GetInitializationArguments() => _weatherAdapterInitializationArgument;

        public override string ToString() => $"Mqtt:WeatherAdapter:{_weatherAdapterInitializationArgument.Identifier}";

        public void Configure(WeatherAdapterInitializationArgument weatherAdapterInitializationArgument)
        {
            _weatherAdapterInitializationArgument = weatherAdapterInitializationArgument;
        }

        public List<IControllerState> GetPossibleStates()
        {
            return new List<IControllerState>
            {
                new ControllerState{ State = "on" , PowerConsumption = 0.1 }
            };
        }
    }
}