using System.Collections.Generic;
using WebControlCenter.CommandAdapter;

namespace WebControlCenter.Services
{
    public interface IMqttAdapterService
    {
        List<MqttAdapterEntry> GetAdapterEntries();
        List<IMqttAdapter> ReadConfiguration();
    }
}