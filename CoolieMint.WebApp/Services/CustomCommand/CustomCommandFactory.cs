using CoolieMint.WebApp.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace CoolieMint.WebApp.Services.CustomCommand
{
    public class CustomCommandFactory
    {
        public Command CreateCommand(CommandDto commandDto)
        {
            return new Command
            {
                Id = commandDto.Id,
                Name = commandDto.Name,
                Description = commandDto.Description,
                Actions = new List<ControllerAction>(commandDto.Actions.Select(a => CreateControllerAction(a)))
            };
        }

        public ControllerAction CreateControllerAction(ActionDto actionDto)
        {
            return new ControllerAction();
        }
    }
}
