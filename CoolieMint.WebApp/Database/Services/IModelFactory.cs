using System;
using System.Collections.Generic;
using WebControlCenter.Database.Models;

namespace WebControlCenter.Database.Services
{
    public interface IModelFactory
    {
        Models.Controller CreateController(Entities.Controller controller);
        Models.ControllerStateInformation CreateControllerStateInformation(Entities.ControllerStateInformation controllerStateInformation);
        Models.ControllerStateInformation CreateControllerStateInformation(Entities.ControllerStateInformation controllerStateInformation, Models.Controller controller);
        List<Models.ControllerStateInformation> CreateControllerStateInformations(List<Entities.ControllerStateInformation> controllerStateInformation);
        Models.ControllerStateHistory CreateControllerStateHistory(Entities.ControllerStateHistory controllerStateHistory);
        Notification CreateNotification(string message, NotificationSeverity severity, DateTime timestamp);
        Notification CreateNotification(Exception exception, DateTime timestamp);
        Notification CreateNotification(Exception exception, string message, DateTime timestamp);
    }
}