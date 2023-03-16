using System.Collections.Generic;

namespace CoolieMint.WebApp.Services.Automation.Rule
{
    public interface IAutomationSceneService
    {
        List<Scene> GetScenes();
        void PersistScenes(List<Scene> scene);
    }
}