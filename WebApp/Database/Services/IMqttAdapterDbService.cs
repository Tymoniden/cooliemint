using System.Collections.Generic;
using WebControlCenter.CommandAdapter;
using WebControlCenter.Database.Entities;

namespace WebControlCenter.Database.Services
{
    public interface IMqttAdapterDbService
    {
        Models.Controller InitializeAdapter(IMqttAdapter mqttAdapter);

        List<Models.ControllerStateInformation> GetStates(Models.Controller controller);
    }
}
