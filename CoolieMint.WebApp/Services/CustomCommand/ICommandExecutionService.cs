using CoolieMint.WebApp.Dtos;
using System.Collections.Generic;

namespace CoolieMint.WebApp.Services.CustomCommand
{
    public interface ICommandExecutionService
    {
        void Excecute(CommandExecutionDto commandExecutionDto);
        List<CommandExecutionDto> GetHistory();
    }
}