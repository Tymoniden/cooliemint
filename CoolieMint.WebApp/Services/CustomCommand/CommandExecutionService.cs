using CoolieMint.WebApp.Dtos;
using System.Collections.Generic;

namespace CoolieMint.WebApp.Services.CustomCommand
{
    public class CommandExecutionService : ICommandExecutionService
    {
        private readonly ICommandExecutionQueueService _commandExecutionQueueService;
        List<CommandExecutionDto> _commandExecutionDtoList = new List<CommandExecutionDto>();

        public CommandExecutionService(ICommandExecutionQueueService commandExecutionQueueService)
        {
            _commandExecutionQueueService = commandExecutionQueueService ?? throw new global::System.ArgumentNullException(nameof(commandExecutionQueueService));
        }

        public void Excecute(CommandExecutionDto commandExecutionDto)
        {
            _commandExecutionQueueService.Enqueue(commandExecutionDto);

            _commandExecutionDtoList.Add(commandExecutionDto);
            if (_commandExecutionDtoList.Count > 100)
            {
                for (int i = 0; i < _commandExecutionDtoList.Count; i++)
                {
                    _commandExecutionDtoList.RemoveAt(i);
                    if (_commandExecutionDtoList.Count <= 100)
                    {
                        break;
                    }
                }
            }
        }

        public List<CommandExecutionDto> GetHistory() => _commandExecutionDtoList;
    }
}
