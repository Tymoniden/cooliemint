using CoolieMint.WebApp.Dtos.Automation;

namespace CoolieMint.WebApp.Services.Automation.Factories
{
    public interface IAutomationDtoFactory
    {
        SceneDto CreateSceneDto(Scene scene);
    }
}