using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using MQTTnet;
using WebControlCenter.CommandAdapter.Enums;
using WebControlCenter.Repository;

namespace WebControlCenter.CommandAdapter.Temperature
{
    public class TemperatureAdapter : IMqttAdapter
    {
        private readonly TemperatureInitializationArgument _initializationArguments;
        private readonly IMessageBroker _messageBroker;
        private double _temperature = 10.0;
        private DateTime _lastTemperatureChange;

        public TemperatureAdapter(TemperatureInitializationArgument initializationArguments, IMessageBroker messageBroker)
        {
            _initializationArguments = initializationArguments;
            _messageBroker = messageBroker;
        }

        public string Identifier => _initializationArguments.Identifier;
        public string Type => "Mqtt:TemperatureAdapter";
        public void AdapterInitialize() { }

        public List<string> MonitoredTopics() => new List<string> { $"{_initializationArguments.TopicPrefix}temperature/{_initializationArguments.Identifier}" };

        public bool CanHandleMqttMessage(string topic) => MonitoredTopics().Contains(topic);

        public void HandleMqttMessage(MqttApplicationMessageReceivedEventArgs eventArguments)
        {
            var message = eventArguments.ApplicationMessage;
            if (message.Topic.ToLower() ==
                $"{_initializationArguments.TopicPrefix}temperature/{_initializationArguments.Identifier}")
            {
                var decodedString = Encoding.UTF8.GetString(eventArguments.ApplicationMessage.Payload);
                _temperature = double.Parse(decodedString, CultureInfo.InvariantCulture);
                _lastTemperatureChange = DateTime.Now;
            }
        }

        public IAdapterStatusMessage GetStatus() => new TemperatureStatus
            {Temperature = _temperature, TimeStamp = _lastTemperatureChange};

        public IAdapterStatusMessage GetStatus(DateTime timestamp)
            =>
                _lastTemperatureChange > timestamp
                    ? new TemperatureStatus
                        {Temperature = _temperature, TimeStamp = _lastTemperatureChange}
                    : null;

        public void HandleWebMessage(string payload)
        {
            throw new NotImplementedException();
        }

        public object GetInitializationArguments() => _initializationArguments;

        public override string ToString() => $"Mqtt:TemperatureAdapter:{_initializationArguments.Identifier}";
        
        public static TemperatureAdapter CreateAdapter(string identifier, IMessageBroker messageBroker) =>
            new TemperatureAdapter(new TemperatureInitializationArgument{ Identifier = identifier }, messageBroker);

        public List<IControllerState> GetPossibleStates()
        {
            return new List<IControllerState>
            {
                new ControllerState{ State = "on" , PowerConsumption = 0.1 }
            };
        }
    }
}