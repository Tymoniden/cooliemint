using System;
using System.Collections.Generic;
using System.Linq;

namespace WebControlCenter.CustomCommand
{
    public class CustomCommandService : ICustomCommandService
    {
        private readonly IControllerActionRegistrationService _controllerActionRegistrationService;

        Dictionary<Command, Action> _registeredCommands = new Dictionary<Command, Action>();

        public CustomCommandService(IControllerActionRegistrationService controllerActionRegistrationService)
        {
            _controllerActionRegistrationService = controllerActionRegistrationService ?? throw new ArgumentNullException(nameof(controllerActionRegistrationService));

            RegisterCommand(new Command
            {
                Id = 0,
                Name = "Timo Büro aus",
                CallingIdentifier = "1",
                Actions = new List<ControllerAction>
                {
                    new ControllerAction
                    {
                        Id = 0,
                        Name = "Sonoff:2:Off",
                        Order = 1
                    },
                    new ControllerAction
                    {
                        Id = 1,
                        Name = "Sonoff:1:Off",
                        Order = 0
                    },
                    new ControllerAction
                    {
                        Id = 2,
                        Name = "Sonoff:3:Off",
                        Order = 2
                    }
                }
            });
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
                actions.Add(_controllerActionRegistrationService.GetControllerAction(x.Name));
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

            _registeredCommands[command].Invoke();
            return true;
        }
    }
}
