using CoolieMint.WebApp.Services.Mqtt;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using WebControlCenter.Controllers;
using WebControlCenter.Database.Services;
using WebControlCenter.Services;

namespace WebControlCenter.CommandAdapter
{
    public class MqttCommandAdapter : IMqttCommandAdapter
    {
        private readonly IMqttAdapterService _mqttAdapterService;
        private readonly IMqttAdapterDbService _mqttAdapterDbService;
        private readonly List<IMqttAdapter> _adapters = new List<IMqttAdapter>();

        public MqttCommandAdapter(IMqttAdapterService mqttAdapterService, IMqttAdapterDbService mqttAdapterDbService)
        {
            _mqttAdapterService = mqttAdapterService ?? throw new ArgumentNullException(nameof(mqttAdapterService));
            _mqttAdapterDbService = mqttAdapterDbService ?? throw new ArgumentNullException(nameof(mqttAdapterDbService));
        }

        public void Initialize() => RegisterKnownAdapters();

        public void Startup() => InitializeAdapters();

        public void RegisterAdapter(IMqttAdapter adapter) => _adapters.Add(adapter);

        void RegisterKnownAdapters()
        {
            foreach (var adapter in _mqttAdapterService.ReadConfiguration())
            {
                RegisterAdapter(adapter);
            }

            // TODO remove or change
            //foreach(var adapter in _adapters)
            //{
            //    _mqttAdapterDbService.InitializeAdapter(adapter);
            //}
        }

        void InitializeAdapters()
        {
            foreach (var adapter in _adapters)
            {
                adapter.AdapterInitialize();
            }
        }

        public void MessageReceive(MqttApplicationMessageReceivedEventArgs eventArguments)
        {
            foreach (var adapter in _adapters.Where(adapter => adapter.CanHandleMqttMessage(eventArguments.ApplicationMessage.Topic)))
            {
                adapter.HandleMqttMessage(eventArguments);
            }
        }

        public List<AdapterStatusMessage> GetAdapterStatusMessages(DateTime timestamp) =>
            GetAdapterStatusMessages(timestamp, _adapters.Select(adapter => adapter.ToString()).ToList());

        public List<AdapterStatusMessage> GetAdapterStatusMessages(DateTime timestamp, List<string> adapters)
        {
            var messages = new List<AdapterStatusMessage>();
            foreach (var adapter in _adapters.Where(a => adapters.Contains(a.ToString())))
            {
                var statusMessage = adapter.GetStatus(timestamp);
                if (statusMessage != null)
                {
                    messages.Add(new AdapterStatusMessage
                    {
                        Identifier = adapter.Identifier,
                        AdapterType = adapter.Type,
                        Timestamp = statusMessage.TimeStamp,
                        Payload = statusMessage
                    });
                }
            }
            return messages;
        }

        public void SendMessage(CommandAdapterMessage commandAdapterMessage)
        {
            foreach(var adapter in _adapters.Where(adapter => adapter.Type == commandAdapterMessage.Adapter && adapter.Identifier == commandAdapterMessage.Id))
            {
                adapter.HandleWebMessage(commandAdapterMessage.Payload);
            }
        }
    }
}