using System;
using WebControlCenter.Database.Entities;

namespace WebControlCenter.Database.Services
{
    public interface IControllerStateService
    {
        ControllerStateHistory ActivateState(ISqLiteContext ctx, ControllerStateInformation controllerStateInformation, DateTime startTime);
        ControllerStateHistory ReplaceState(ISqLiteContext ctx, Models.ControllerStateInformation newStateModel, Models.ControllerStateInformation oldStateModel);
        Models.ControllerStateHistory ReplaceState(Models.ControllerStateInformation newStateModel, Models.ControllerStateInformation oldStateModel);
        DateTime TerminateState(ISqLiteContext ctx, ControllerStateInformation state);
    }
}