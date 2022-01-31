using CoolieMint.WebApp.Dtos;
using CoolieMint.WebApp.Services;
using CoolieMint.WebApp.Services.CustomCommand;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoolieMint.WebApp.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class CommandExecutionController : Controller
    {
        private readonly ICommandExecutionService _commandExecutionService;
        private readonly ICustomCommandService _customCommandService;
        private readonly ICommandDtoFactory _commandDtoFactory;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CommandExecutionController(ICommandExecutionService commandExecutionService, ICustomCommandService customCommandService, ICommandDtoFactory commandDtoFactory, IDateTimeProvider dateTimeProvider)
        {
            _commandExecutionService = commandExecutionService ?? throw new System.ArgumentNullException(nameof(commandExecutionService));
            _customCommandService = customCommandService ?? throw new ArgumentNullException(nameof(customCommandService));
            _commandDtoFactory = commandDtoFactory;
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));

            foreach (var command in _customCommandService.GetCommands())
            {
                _commandExecutionService.Excecute(new CommandExecutionDto { Command = _commandDtoFactory.CreateCommandDto(command) });
            }
        }

        [HttpGet]
        public JsonResult Get()
        {
            return Json(_commandExecutionService.GetHistory());
        }

        [HttpPost]
        public JsonResult Post([FromBody] CommandExecutionDto commandExecutionDto)
        {
            if (commandExecutionDto is null)
            {
                return Json("NOK");
            }

            commandExecutionDto.ExecutionTimestamp ??= _dateTimeProvider.UtcNow();

            _commandExecutionService.Excecute(commandExecutionDto);
            return Json("OK");
        }
    }
}
