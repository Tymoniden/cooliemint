using System;
using System.Collections.Generic;
using WebControlCenter.Services.Log;

namespace WebControlCenter.CustomCommand
{
    public class ControllerActionRegistrationService : IControllerActionRegistrationService
    {
        private readonly ILogService _logService;

        Dictionary<string, Action> _knownActions = new Dictionary<string, Action>();

        public ControllerActionRegistrationService(ILogService logService)
        {
            this._logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        public Action GetControllerAction(string action)
        {
            if (!_knownActions.ContainsKey(action))
            {
                // TODO think about it!
                return () => { _logService.LogInfo($"{action} was called!"); };
            }

            return _knownActions[action];
        }
    }
}
