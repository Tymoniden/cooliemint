using CoolieMint.WebApp.Services.CustomCommand;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoolieMint.WebApp.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class CommandController : Controller
    {
        private readonly ICustomCommandService _customCommandService;
        private readonly ICommandDtoFactory _commandDtoFactory;
        private readonly ICommandFactory _commandFactory;

        public CommandController(ICustomCommandService customCommandService, ICommandDtoFactory commandDtoFactory, ICommandFactory commandFactory)
        {
            _customCommandService = customCommandService ?? throw new System.ArgumentNullException(nameof(customCommandService));
            _commandDtoFactory = commandDtoFactory ?? throw new System.ArgumentNullException(nameof(commandDtoFactory));
            _commandFactory = commandFactory ?? throw new System.ArgumentNullException(nameof(commandFactory));
        }

        [HttpGet]
        public JsonResult Get()
        {
            var commands = _customCommandService.GetCommands();
            var commandDtos = commands.Select(c => _commandDtoFactory.CreateCommandDto(c)).ToList();

            _customCommandService.PersistChanges();

            return Json(commandDtos);
        }

        [HttpPost]
        public JsonResult Post([FromBody] CommandDto[] commands)
        {
            return Json("OK");
        }

        [HttpPut]
        public JsonResult Put([FromBody] Dtos.CommandDto dto, int id)
        {
            try
            {
                var command = _commandFactory.CreateCommand(dto);
                _customCommandService.RegisterCommand(command);
                _customCommandService.PersistChanges();
                return Json(command);
            }
            catch
            {
                return Json("NOK");
            }
        }
    }

    public class CommandDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
}
