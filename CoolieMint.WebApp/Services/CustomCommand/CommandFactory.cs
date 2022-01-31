using CoolieMint.WebApp.Dtos;
using System.Linq;

namespace CoolieMint.WebApp.Services.CustomCommand
{
    public class CommandFactory : ICommandFactory
    {
        public Command CreateCommand(CommandDto dto)
        {
            return new Command
            {
                Id = dto.Id,
                Description = dto.Description,
                Name = dto.Name,
                Actions = dto.Actions.Select(a => CreateAction(a)).ToList(),
            };
        }

        public ControllerAction CreateAction(ActionDto action)
        {
            return new ControllerAction
            {
                Order = action.Order,
                DisplayName = action.DisplayName,
                Name = action.Execute
            };
        }
    }
}
