using CoolieMint.WebApp.Services.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebControlCenter.Services;
using WebControlCenter.Services.Log;
using WebControlCenter.Services.SmartDevice;

namespace CoolieMint.WebApp.Services.CustomCommand
{
    public class CustomCommandService : ICustomCommandService
    {
        readonly IDeviceOperationProvider _deviceOperationProvider;
        readonly ILogService _logService;
        private readonly IFileSystemService _fileSystemService;
        private readonly IJsonSerializerService _jsonSerializerService;
        readonly Dictionary<Command, Action> _registeredCommands = new();
        readonly Dictionary<Command, Func<Task>> _registeredAsyncCommands = new();
        readonly List<string> _executingCommand = new();
        readonly List<long> _executingAsyncCommand = new();

        public CustomCommandService(IDeviceOperationProvider deviceOperationProvider, ILogService logService, IFileSystemService fileSystemService, IJsonSerializerService jsonSerializerService)
        {
            _deviceOperationProvider = deviceOperationProvider ?? throw new ArgumentNullException(nameof(deviceOperationProvider));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
            _jsonSerializerService = jsonSerializerService ?? throw new ArgumentNullException(nameof(jsonSerializerService));
        }

        public List<Command> GetCommands() => _registeredCommands.Keys.ToList();

        public Command GetCommand(string callingIdentifier)
            => _registeredCommands.Keys.FirstOrDefault(cmd => cmd.CallingIdentifier.Equals(callingIdentifier, StringComparison.InvariantCultureIgnoreCase));

        public Command GetCommand(long id) => _registeredCommands.Keys.FirstOrDefault(key => key.Id == id);

        #region tobedeleted
        // TODO Delete
        //public void RegisterCommand(Command command)
        //{
        //    if (command is null)
        //    {
        //        throw new ArgumentNullException(nameof(command));
        //    }

        //    var actions = new List<Action>();
        //    foreach (var x in command.Actions.OrderBy(a => a.Order))
        //    {
        //        var operation = _deviceOperationProvider.GetOperation(x.Name);
        //        if (operation == null)
        //        {
        //            continue;
        //        }

        //        actions.Add(operation.Action);
        //    }

        //    Action commandAction = () =>
        //    {
        //        foreach (var action in actions)
        //        {
        //            action.Invoke();
        //        }
        //    };

        //    _registeredCommands.Add(command, commandAction);
        //}

        //public bool ExecuteCommand(Command command)
        //{
        //    if (!_registeredCommands.ContainsKey(command))
        //    {
        //        return false;
        //    }

        //    if (_executingCommand.Contains(command.CallingIdentifier))
        //    {
        //        return true;
        //    }

        //    lock (_executingCommand)
        //    {
        //        _executingCommand.Add(command.CallingIdentifier);
        //    }

        //    _logService.LogInfo($"Executing command {command.Name}");
        //    _registeredCommands[command].Invoke();

        //    lock (_executingCommand)
        //    {
        //        _executingCommand.Remove(command.CallingIdentifier);
        //    }
        //    return true;
        //}
        #endregion

        public void RegisterCommand(Command command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            // new command
            if(command.Id == 0)
            {
                var lastCommandId = _registeredAsyncCommands.Keys.OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefault();
                command.Id = lastCommandId != 0 ? lastCommandId + 1 : 1;
            }

            if(_registeredAsyncCommands.All(c => c.Key.Id != command.Id))
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
            var commands = GetCommands();
            var fileContent = _jsonSerializerService.Serialize(commands);
            // TODO what about directory provider?
            var configFile = _fileSystemService.CombinePath(_fileSystemService.GetConfigurationPath(), "custom.commands.json");
            _fileSystemService.WriteAllText(configFile, fileContent);
        }
    }
}
