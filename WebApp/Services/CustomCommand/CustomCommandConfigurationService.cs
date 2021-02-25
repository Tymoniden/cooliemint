using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebControlCenter.CustomCommand;

namespace WebControlCenter.Services.CustomCommand
{
    public class CustomCommandConfigurationService : ICustomCommandConfigurationService
    {
        private const string _configurationFile = "customCommands.json";
        private readonly IFileSystemService _fileSystemService;
        private readonly IJsonSerializerService _jsonSerializerService;
        private readonly ICustomCommandService _customCommandService;

        public CustomCommandConfigurationService(IFileSystemService fileSystemService, IJsonSerializerService jsonSerializerService, ICustomCommandService customCommandService)
        {
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
            _jsonSerializerService = jsonSerializerService ?? throw new ArgumentNullException(nameof(jsonSerializerService));
            _customCommandService = customCommandService ?? throw new ArgumentNullException(nameof(customCommandService));
        }

        public List<Command> ReadCustomCommands()
        {
            var configFile = _fileSystemService.CombinePath(_fileSystemService.GetConfigurationPath(), _configurationFile);
            var content = _fileSystemService.ReadFileAsString(configFile);
            var commands = _jsonSerializerService.Deserialize<Command[]>(content);
            return commands.ToList();
        }

        void WriteCustomCommands(List<Command> commands)
        {
            var configFile = _fileSystemService.CombinePath(_fileSystemService.GetConfigurationPath(), _configurationFile);
            var content = _jsonSerializerService.Serialize(commands);
            _fileSystemService.WriteAllText(configFile, content);
        }

        public void ReloadConfiguration()
        {
            foreach (var command in ReadCustomCommands())
            {
                _customCommandService.RegisterCommand(command);
            }
        }
    }
}
