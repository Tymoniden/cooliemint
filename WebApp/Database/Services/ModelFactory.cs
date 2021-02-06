using System;
using System.Collections.Generic;
using WebControlCenter.Database.Models;

namespace WebControlCenter.Database.Services
{
    public class ModelFactory : IModelFactory
    {
        long _notificationId = 1;

        public Controller CreateController(Entities.Controller controller)
        {
            var controllerModel = new Controller()
            {
                Id = controller.Id,
                Identifier = controller.Identifier,
                Type = controller.Type,

            };

            return controllerModel;
        }

        Controller CreateController(Entities.Controller controller, bool createChildren)
        {
            var controllerModel = CreateController(controller);
            if (createChildren)
            {
                foreach (var stateInformation in controller.ControllerStateInformations)
                {
                    controllerModel.ControllerStateInformations.Add(CreateControllerStateInformation(stateInformation, controllerModel));
                }
            }

            foreach(var info in controllerModel.ControllerStateInformations)
            {
                info.Controller = controllerModel;
            }

            return controllerModel;
        }

        public ControllerStateInformation CreateControllerStateInformation(Entities.ControllerStateInformation controllerStateInformation)
        {
            return new ControllerStateInformation()
            {
                Id = controllerStateInformation.Id,
                Controller = (controllerStateInformation.Controller != null) ? CreateController(controllerStateInformation.Controller, false) : null,
                ControllerId = controllerStateInformation.ControllerId,
                PowerConsumption = controllerStateInformation.PowerConsumption,
                State = controllerStateInformation.State
            };
        }

        public ControllerStateInformation CreateControllerStateInformation(Entities.ControllerStateInformation controllerStateInformation, Controller controller)
        {
            var stateInformation = CreateControllerStateInformation(controllerStateInformation);
            stateInformation.Controller = controller;

            return stateInformation;
        }

        public List<ControllerStateInformation> CreateControllerStateInformations(List<Entities.ControllerStateInformation> controllerStateInformation)
        {
            var stateInformations = new List<ControllerStateInformation>();
            foreach(var stateInformation in controllerStateInformation)
            {
                stateInformations.Add(CreateControllerStateInformation(stateInformation));
            }

            return stateInformations;
        }

        public ControllerStateHistory CreateControllerStateHistory(Entities.ControllerStateHistory controllerStateHistory)
        {
            return new ControllerStateHistory
            {
                Id = controllerStateHistory.Id,
                ControllerStateInformationId = controllerStateHistory.ControllerStateInformationId,
                StartTime = controllerStateHistory.StartTime,
                EndTime = controllerStateHistory.EndTime
            };
        }

        public Notification CreateNotification(string message, NotificationSeverity severity, DateTime timestamp)
        {
            return new Notification
            {
                Id = _notificationId++,
                Message = message,
                NotificationSeverity = severity,
                Timestamp = timestamp
            };
        }

        public Notification CreateNotification(Exception exception, DateTime timestamp)
        {
            return CreateNotification(exception.ToString(), NotificationSeverity.Error, timestamp);
        }

        public Notification CreateNotification(Exception exception, string message, DateTime timestamp)
        {
            return CreateNotification($"{message}{Environment.NewLine}{exception.ToString()}", NotificationSeverity.Error, timestamp);
        }
    }
}
