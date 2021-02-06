

using System;
using WebControlCenter.Database.Entities;

namespace WebControlCenter.Database.Services
{
    public interface IEntityFactory
    {
        Controller CreateController();

        ControllerStateHistory CreateStateHistoryFromId(long stateId);

        ControllerStateHistory CreateStateHistory(ControllerStateInformation controllerStateInformation);

        ControllerStateHistory CreateStateHistory(ControllerStateInformation controllerStateInformation, DateTime startTime);

        ControllerStateInformation CreateControllerStateInformation(Models.ControllerStateInformation controllerStateInformation);
    }
}