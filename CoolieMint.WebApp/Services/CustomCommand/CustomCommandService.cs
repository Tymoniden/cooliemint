using CoolieMint.WebApp.Services.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebControlCenter.Services.Log;
using WebControlCenter.Services.SmartDevice;

namespace CoolieMint.WebApp.Services.CustomCommand
{
    public class CustomCommandService : ICustomCommandService
    {
        readonly IDeviceOperationProvider _deviceOperationProvider;
        readonly IFileNameProvider _fileNameProvider;
        readonly ILogService _logService;
        readonly IJsonPersistenceService _jsonPersistenceService;
        readonly Dictionary<Command, Action> _registeredCommands = new();
        readonly Dictionary<Command, Func<Task>> _registeredAsyncCommands = new();
        readonly List<string> _executingCommand = new();
        readonly List<long> _executingAsyncCommand = new();

        public CustomCommandService(
            IDeviceOperationProvider deviceOperationProvider,
            IFileNameProvider fileNameProvider,
            IJsonPersistenceService jsonPersistenceService, 
            ILogService logService)
        {
            _deviceOperationProvider = deviceOperationProvider ?? throw new ArgumentNullException(nameof(deviceOperationProvider));
            _fileNameProvider = fileNameProvider ?? throw new ArgumentNullException(nameof(fileNameProvider));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _jsonPersistenceService = jsonPersistenceService ?? throw new ArgumentNullException(nameof(jsonPersistenceService));
        }

        public List<Command> GetCommands() => _registeredCommands.Keys.ToList();

        public Command GetCommand(string callingIdentifier)
            => _registeredCommands.Keys.FirstOrDefault(cmd => cmd.CallingIdentifier.Equals(callingIdentifier, StringComparison.InvariantCultureIgnoreCase));

        public Command GetCommand(long id) => _registeredCommands.Keys.FirstOrDefault(key => key.Id == id);

        public void RegisterCommand(Command command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            // new command
            if (command.Id == 0)
            {
                var lastCommandId = _registeredAsyncCommands.Keys.OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefault();
                command.Id = lastCommandId != 0 ? lastCommandId + 1 : 1;
            }

            if (_registeredAsyncCommands.All(c => c.Key.Id != command.Id))
            {
                _registeredAsyncCommands.Add(command, null);
            }

            var actions = new List<Action>();
            foreach (var x in command.Actions.OrderBy(a => a.Order))
            {
                var operation = _deviceOperationProvider.GetOperation(x.Name);
                if (operation == null)
                {
                    continue;
                }

                actions.Add(operation.Action);
            }

            Func<Task> commandAction = async () =>
            {
                await Task.Run(() =>
                {
                    foreach (var action in actions)
                    {
                        try
                        {
                            action.Invoke();
                        }
                        catch (Exception ex)
                        {
                            // TODO get actions a description and log it for this error case
                            _logService.LogException(ex, $"Error while executing action of command: {command.Id}");
                        }
                    }
                });
            };

            _registeredAsyncCommands[command] = commandAction;
        }

        public async Task<bool> ExecuteCommand(long commandId, CancellationToken cancellationToken)
        {
            var command = _registeredAsyncCommands.Keys.FirstOrDefault(command => command.Id == commandId);
            if (command == null)
            {
                _logService.LogInfo($"Command not found with id: {commandId}");
                return false;
            }

            if (_executingAsyncCommand.Contains(commandId))
            {
                _logService.LogInfo($"Command with id: {commandId} is already executing. Was not executed twice.");
                return true;
            }

            lock (_executingCommand)
            {
                _executingAsyncCommand.Add(commandId);
            }

            try
            {
                _logService.LogInfo($"Executing command {command.Name}");
                await _registeredAsyncCommands[command].Invoke();
            }
            catch (Exception ex)
            {
                _logService?.LogException(ex);
            }

            lock (_executingCommand)
            {
                _executingAsyncCommand.Remove(commandId);
            }
            return true;
        }

        // TODO extract method to a different service? -> custom command storage
        public void PersistChanges()
        {
            _jsonPersistenceService.PersistObject(GetCommands(), _fileNameProvider.GetCustomCommandFile());
        }
    }
}
