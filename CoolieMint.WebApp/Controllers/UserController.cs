using Microsoft.AspNetCore.Mvc;
using WebControlCenter.Services;
using WebControlCenter.Services.Rest;

namespace WebControlCenter.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUiConfigurationService _uiConfigurationService;
        private readonly IUserFactory _userFactory;

        public UserController(IUiConfigurationService uiConfigurationService, IUserFactory userFactory)
        {
            _uiConfigurationService = uiConfigurationService ?? throw new System.ArgumentNullException(nameof(uiConfigurationService));
            _userFactory = userFactory ?? throw new System.ArgumentNullException(nameof(userFactory));
        }

        public JsonResult Index()
        {
            var configuration = _uiConfigurationService.GetConfiguration();

            return new JsonResult(_userFactory.CreateUsers(configuration));
        }
    }
}
