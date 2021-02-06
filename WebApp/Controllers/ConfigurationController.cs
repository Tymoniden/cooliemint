using Microsoft.AspNetCore.Mvc;
using WebControlCenter.Services;

namespace WebControlCenter.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IUiConfigurationService _uiConfigurationService;
        private readonly IAdapterSettingService _adapterSettingService;

        public ConfigurationController(IUiConfigurationService uiConfigurationService, IAdapterSettingService adapterSettingService)
        {
            _uiConfigurationService = uiConfigurationService;
            _adapterSettingService = adapterSettingService;
        }

        public IActionResult Index()
        {
            return null;
        }

        [HttpGet("Reload")]
        public JsonResult Reload()
        {
            _adapterSettingService.ResetSettingsConfiguration();
            _uiConfigurationService.ReadAllConfigurationFiles();
            return new JsonResult(new {foo="bar", baz="Blech"});
        }
    }
}
