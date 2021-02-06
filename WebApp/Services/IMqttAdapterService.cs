using System.Collections.Generic;
using WebControlCenter.CommandAdapter;

namespace WebControlCenter.Services
{
    public interface IMqttAdapterService
    {
        List<IMqttAdapter> ReadConfiguration();
    }
}