using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebControlCenter.CommandAdapter;
using WebControlCenter.Controllers;

namespace WebControlCenter.Services
{
    public class AdapterService : IAdapterService
    {
        private readonly List<ICommandAdapter> _commandAdapters = new List<ICommandAdapter>();

        public AdapterService(IMqttCommandAdapter mqttCommandAdapter)
        {
            _commandAdapters.Add(mqttCommandAdapter);
        }

        public List<AdapterStatusMessage> GetUpdates(DateTime referenceTimestamp) =>
            _commandAdapters.SelectMany(ca => ca.GetAdapterStatusMessages(referenceTimestamp)).ToList();

        public List<AdapterStatusMessage> GetUpdates(DateTime referenceTimestamp, List<string> adapters) =>
            _commandAdapters.SelectMany(ca => ca.GetAdapterStatusMessages(referenceTimestamp, adapters)).ToList();

        public void SendMessage(CommandAdapterMessage commandAdapterMessage)
        {
            foreach(var commandAdapter in _commandAdapters)
            {
                commandAdapter.SendMessage(commandAdapterMessage);
            }
        }
    }
}
