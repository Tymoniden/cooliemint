using CoolieMint.WebApp.Services.Automation;
using CoolieMint.WebApp.Services.Automation.Factories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoolieMint.WebApp.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class SceneController : Controller
    {
        private readonly IAutomationRulesStore _automationRulesStore;
        private readonly IAutomationDtoFactory _automationDtoFactory;

        public SceneController(IAutomationRulesStore automationRulesStore, IAutomationDtoFactory automationDtoFactory)
        {
            _automationRulesStore = automationRulesStore ?? throw new System.ArgumentNullException(nameof(automationRulesStore));
            _automationDtoFactory = automationDtoFactory ?? throw new System.ArgumentNullException(nameof(automationDtoFactory));
        }

        [HttpGet]
        public JsonResult Get()
        {
            var scenes = _automationRulesStore.GetScenes().Select(scene => _automationDtoFactory.CreateSceneDto(scene)).ToList();
            return Json(scenes);
        }
    }
}
