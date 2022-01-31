using CoolieMint.WebApp.Dtos;

namespace CoolieMint.WebApp.Services.CustomCommand
{
    public interface ICommandDtoFactory
    {
        ActionDto CreateActionDto(ControllerAction action);
        CommandDto CreateCommandDto(Command command);
    }
}