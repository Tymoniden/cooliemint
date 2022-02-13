using CoolieMint.WebApp.Services.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoolieMint.WebApp.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class HistoryEntryController : ControllerBase
    {
        private readonly ISystemStateCache _systemStateCache;

        public HistoryEntryController(ISystemStateCache systemStateCache)
        {
            _systemStateCache = systemStateCache ?? throw new System.ArgumentNullException(nameof(systemStateCache));
        }

        [HttpGet]
        public void Get()
        {
            _systemStateCache.GetAll();
        }
    }
}
