using System.Collections.Generic;
using WebControlCenter.CommandAdapter;
using WebControlCenter.Services;

namespace CoolieMint.WebApp.Services.Mqtt
{
    public interface IMqttAdapterService
    {
        List<MqttAdapterEntry> GetAdapterEntries();
        List<IMqttAdapter> ReadConfiguration();
    }
}