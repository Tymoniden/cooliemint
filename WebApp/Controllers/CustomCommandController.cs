using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebControlCenter.CustomCommand;

namespace WebControlCenter.Controllers
{
    public class CustomCommandController : Controller
    {
        private readonly ICustomCommandService _customCommandService;

        public CustomCommandController(ICustomCommandService customCommandService)
        {
            _customCommandService = customCommandService ?? throw new ArgumentNullException(nameof(customCommandService));
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("[controller]/[action]/{callingIdentifier}")]
        public IActionResult Execute(string callingIdentifier)
        {
            var command = _customCommandService.GetCommand(callingIdentifier);
            if(command != null)
            {
                _customCommandService.ExecuteCommand(command);

                return Content("OK");
            }

            return Content("NOK");
        }
    }
}
