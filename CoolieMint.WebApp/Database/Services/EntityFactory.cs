

using System;
using WebControlCenter.Database.Entities;

namespace WebControlCenter.Database.Services
{

    public class EntityFactory : IEntityFactory
    {
        public Controller CreateController()
        {
            return new Controller();
        }

        public ControllerStateInformation CreateControllerStateInformation(Models.ControllerStateInformation controllerStateInformation)
        {
            return new ControllerStateInformation()
            {
                //Controller
                ControllerId = controllerStateInformation.ControllerId,
                Id = controllerStateInformation.Id,
                PowerConsumption = controllerStateInformation.PowerConsumption,
                State = controllerStateInformation.State
            };
        }

        public ControllerStateHistory CreateStateHistory(ControllerStateInformation controllerStateInformation)
        {
            return new ControllerStateHistory()
            {
                ControllerStateInformationId = controllerStateInformation.Id
            };
        }

        public ControllerStateHistory CreateStateHistory(ControllerStateInformation controllerStateInformation, DateTime startTime)
        {
            var history = CreateStateHistory(controllerStateInformation);
            history.StartTime = startTime;

            return history;
        }

        public ControllerStateHistory CreateStateHistoryFromId(long stateId)
        {
            return new ControllerStateHistory()
            {
                ControllerStateInformationId = stateId
            };
        }
    }
}