using CoolieMint.WebApp.Dtos;

namespace CoolieMint.WebApp.Services.CustomCommand
{
    public interface ICommandFactory
    {
        ControllerAction CreateAction(ActionDto action);
        Command CreateCommand(CommandDto dto);
    }
}