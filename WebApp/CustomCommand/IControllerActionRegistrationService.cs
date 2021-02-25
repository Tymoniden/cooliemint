using System;

namespace WebControlCenter.CustomCommand
{
    public interface IControllerActionRegistrationService
    {
        Action GetControllerAction(string action);
    }
}