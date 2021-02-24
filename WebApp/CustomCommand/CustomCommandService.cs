using System;
using System.Collections.Generic;
using System.Linq;
using WebControlCenter.Services.Log;
using WebControlCenter.Services.SmartDevice;

namespace WebControlCenter.CustomCommand
{
    public class CustomCommandService : ICustomCommandService
    {
        private readonly IControllerActionRegistrationService _controllerActionRegistrationService;
        private readonly IDeviceOperationProvider _deviceOperationProvider;
        private readonly ILogService _logService;
        Dictionary<Command, Action> _registeredCommands = new Dictionary<Command, Action>();

        public CustomCommandService(IControllerActionRegistrationService controllerActionRegistrationService, IDeviceOperationProvider deviceOperationProvider, ILogService logService)
        {
            _controllerActionRegistrationService = controllerActionRegistrationService ?? throw new ArgumentNullException(nameof(controllerActionRegistrationService));
            _deviceOperationProvider = deviceOperationProvider ?? throw new ArgumentNullException(nameof(deviceOperationProvider));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        public Command GetCommand(string callingIdentifier)
            => _registeredCommands.Keys.FirstOrDefault(cmd => cmd.CallingIdentifier.Equals(callingIdentifier, StringComparison.InvariantCultureIgnoreCase));

        public void RegisterCommand(Command command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var actions = new List<Action>();
            foreach (var x in command.Actions.OrderBy(a => a.Order))
            {
                var operation = _deviceOperationProvider.GetOperation(x.Name);
                if(operation == null)
                {
                    continue;
                }

                actions.Add(operation.Action);
            }

            Action commandAction = () =>
            {
                foreach (var action in actions)
                {
                    action.Invoke();
                }
            };

            _registeredCommands.Add(command, commandAction);
        }

        public bool ExecuteCommand(Command command)
        {
            if (!_registeredCommands.ContainsKey(command))
            {
                return false;
            }

            _logService.LogInfo($"Executing command {command.Name}");
            _registeredCommands[command].Invoke();
            return true;
        }
    }
}
