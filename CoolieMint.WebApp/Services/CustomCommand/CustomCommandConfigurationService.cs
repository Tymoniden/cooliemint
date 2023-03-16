using CoolieMint.WebApp.Services.CustomCommand;
using CoolieMint.WebApp.Services.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebControlCenter.Services.CustomCommand
{
    public class CustomCommandConfigurationService : ICustomCommandConfigurationService
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly IFileNameProvider _fileNameProvider;
        private readonly IJsonSerializerService _jsonSerializerService;
        private readonly ICustomCommandService _customCommandService;

        public CustomCommandConfigurationService(IFileSystemService fileSystemService, IFileNameProvider fileNameProvider, IJsonSerializerService jsonSerializerService, ICustomCommandService customCommandService)
        {
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
            _fileNameProvider = fileNameProvider ?? throw new ArgumentNullException(nameof(fileNameProvider));
            _jsonSerializerService = jsonSerializerService ?? throw new ArgumentNullException(nameof(jsonSerializerService));
            _customCommandService = customCommandService ?? throw new ArgumentNullException(nameof(customCommandService));
        }

        public List<Command> ReadCustomCommands()
        {
            var configFile = _fileNameProvider.GetCustomCommandLegacyFile();
            var content = _fileSystemService.ReadFileAsString(configFile);
            var commands = _jsonSerializerService.Deserialize<Command[]>(content);
            return commands.ToList();
        }

        void WriteCustomCommands(List<Command> commands)
        {
            var configFile = _fileNameProvider.GetCustomCommandLegacyFile();
            var content = _jsonSerializerService.Serialize(commands, SerializerSettings.FileSerializer);
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
