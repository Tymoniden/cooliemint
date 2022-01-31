using Microsoft.AspNetCore.Mvc;
using WebControlCenter.Services.Log;

namespace WebControlCenter.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogFileService _logFileService;

        public LogController(ILogFileService logFileService)
        {
            _logFileService = logFileService ?? throw new System.ArgumentNullException(nameof(logFileService));
        }

        public IActionResult Index()
        {
            return Content(_logFileService.GetLogs());
        }
    }
}
