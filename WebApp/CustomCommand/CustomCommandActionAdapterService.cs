using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebControlCenter.CommandAdapter;
using WebControlCenter.CommandAdapter.Sonoff;

namespace WebControlCenter.CustomCommand
{
    public class CustomCommandActionAdapterService
    {
        void GetActions(IMqttAdapter mqttAdapter)
        {
            if(mqttAdapter is SonoffAdapter adapter)
            {
                GetSonoffActions(adapter);
            }
        }

        void GetSonoffActions(SonoffAdapter adapter)
        {

        }
    }
}
