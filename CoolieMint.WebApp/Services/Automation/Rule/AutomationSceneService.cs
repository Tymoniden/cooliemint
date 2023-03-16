using CoolieMint.WebApp.Services.FileSystem;
using System.Collections.Generic;
using WebControlCenter.Services;

namespace CoolieMint.WebApp.Services.Automation.Rule
{
    public class AutomationSceneService : IAutomationSceneService
    {
        readonly IFileSystemService _fileSystemService;
        readonly IFileNameProvider _fileNameProvider;
        readonly IJsonSerializerService _jsonSerializerService;

        public AutomationSceneService(IFileSystemService fileSystemService, IFileNameProvider fileNameProvider, IJsonSerializerService jsonSerializerService)
        {
            _fileSystemService = fileSystemService ?? throw new System.ArgumentNullException(nameof(fileSystemService));
            _fileNameProvider = fileNameProvider ?? throw new System.ArgumentNullException(nameof(fileNameProvider));
            _jsonSerializerService = jsonSerializerService ?? throw new System.ArgumentNullException(nameof(jsonSerializerService));
        }

        public void PersistScenes(List<Scene> scene)
        {
            _fileSystemService.WriteAllText(_fileNameProvider.GetScenesConfigFile(), _jsonSerializerService.Serialize(scene, SerializerSettings.FileSerializer));
        }

        public List<Scene> GetScenes()
        {
            var content = _fileSystemService.ReadFileAsString(_fileNameProvider.GetScenesConfigFile());
            return _jsonSerializerService.Deserialize<List<Scene>>(content);
        }
    }
}
