using CoolieMint.WebApp.Services.CustomCommand;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebControlCenter.Controllers
{
    public class CustomCommandController : Controller
    {
        private readonly ICustomCommandService _customCommandService;

        public CustomCommandController(ICustomCommandService customCommandService)
        {
            _customCommandService = customCommandService ?? throw new ArgumentNullException(nameof(customCommandService));
        }

        [Route("[controller]/[action]/{callingIdentifier}")]
        public async Task<IActionResult> Execute(string callingIdentifier)
        {
            if(long.TryParse(callingIdentifier, out var commandId))
            {
                if (await _customCommandService.ExecuteCommand(commandId, CancellationToken.None))
                {
                    return Content("OK");
                }
            }
            
            return Content("NOK");
        }
    }
}
