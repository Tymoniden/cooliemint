using System.Collections.Generic;

namespace CoolieMint.WebApp.Services.Automation
{
    public interface IAutomationRulesStore
    {
        void AddScene(Scene rule, bool replace = true);
        List<Scene> GetScenes();
    }
}