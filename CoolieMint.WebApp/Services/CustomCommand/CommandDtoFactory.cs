using CoolieMint.WebApp.Dtos;
using System.Linq;

namespace CoolieMint.WebApp.Services.CustomCommand
{
    public class CommandDtoFactory : ICommandDtoFactory
    {
        public CommandDto CreateCommandDto(Command command)
        {
            var dto = new CommandDto
            {
                Id = command.Id,
                Name = command.Name,
                Description = command.Description,
                Actions = command.Actions.Select(a => CreateActionDto(a)).ToList(),
            };

            return dto;
        }

        public ActionDto CreateActionDto(ControllerAction action)
        {
            return new ActionDto
            {
                Execute = action.Name,
                Order = action.Order,
                DisplayName = action.DisplayName
            };
        }
    }
}
