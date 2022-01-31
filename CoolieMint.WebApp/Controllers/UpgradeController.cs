using CoolieMint.WebApp.Services.SystemUpgrade;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebControlCenter.Services;
using WebControlCenter.Services.Log;

namespace WebControlCenter.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class UpgradeController : Controller
    {
        private readonly ICooliemintPackageService _cooliemintPackageService;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly ILogService _logService;

        public UpgradeController(ICooliemintPackageService cooliemintPackageService, IHostApplicationLifetime appLifetime, ILogService logService)
        {
            _cooliemintPackageService = cooliemintPackageService ?? throw new ArgumentNullException(nameof(cooliemintPackageService));
            _appLifetime = appLifetime ?? throw new ArgumentNullException(nameof(appLifetime));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        public JsonResult Post([FromBody] UpgradeModel upgrade)
        {
            try
            {
                _logService.LogInfo($"[UpgradeController] upgrade to version {upgrade.Version}.");
                _logService.LogInfo($"[UpgradeController] extracting package");
                _cooliemintPackageService.SavePackage(upgrade.Content);
                _logService.LogInfo($"[UpgradeController] package extracted.");
                _logService.LogInfo($"[UpgradeController] Stopping application due to upgrade.");
                _appLifetime.StopApplication();
                return Json(new { Status = "OK", Message = $"Successfully extracted package. Initializing upgrade." });
            }
            catch(Exception)
            {
                return Json(new { Status = "NOK", Message = $"Could not upgrade to package." });
            }
        }
    }

    public class UpgradeModel
    {
        public byte[] Content { get; set; }

        public string Version { get; set; }
    }
}
