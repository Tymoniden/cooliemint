using CoolieMint.WebApp.Services.Automation;
using CoolieMint.WebApp.Services.Automation.Factories;
using CoolieMint.WebApp.Services.Automation.Rule;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using WebControlCenter.Services;

namespace CoolieMint.WebApp.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class SceneController : Controller
    {
        private readonly IJsonSerializerService _jsonSerializerService;
        private readonly IAutomationRulesStore _automationRulesStore;
        private readonly IAutomationDtoFactory _automationDtoFactory;

        public SceneController(IJsonSerializerService jsonSerializerService, IAutomationRulesStore automationRulesStore, IAutomationDtoFactory automationDtoFactory)
        {
            _jsonSerializerService = jsonSerializerService ?? throw new System.ArgumentNullException(nameof(jsonSerializerService));
            _automationRulesStore = automationRulesStore ?? throw new System.ArgumentNullException(nameof(automationRulesStore));
            _automationDtoFactory = automationDtoFactory ?? throw new System.ArgumentNullException(nameof(automationDtoFactory));
        }

        [HttpGet]
        public ActionResult Get()
        {
            var scenes = _automationRulesStore.GetScenes().Select(scene => _automationDtoFactory.CreateSceneDto(scene)).ToList();

            var serializedScenes = _jsonSerializerService.Serialize(scenes, SerializerSettings.ApiSerializer);

            return Content(serializedScenes, "application/json");
        }
    }
}
