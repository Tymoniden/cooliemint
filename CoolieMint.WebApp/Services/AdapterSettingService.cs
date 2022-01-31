using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebControlCenter.Services
{
    public class AdapterSettingService : IAdapterSettingService
    {
        private readonly IControlModelService _controlModelService;
        readonly Dictionary<string, List<string>> _configurationMapping = new Dictionary<string, List<string>>();

        public AdapterSettingService(IControlModelService controlModelService)
        {
            _controlModelService = controlModelService;
        }

        public string CreateAdapterConfiguration()
        {
            var id = Guid.NewGuid().ToString();
            _configurationMapping.Add(id, new List<string>());
            return id;
        }

        public void AddAdapter(string id, string adapter)
        {
            if (!_configurationMapping.ContainsKey(id))
            {
                throw new ArgumentException($"{nameof(id)} was not found");
            }

            if (_configurationMapping[id].Contains(adapter))
            {
                return;
            }

            _configurationMapping[id].Add(adapter);
        }

        public void AddAdapters(string id, List<string> adapters)
        {
            if (!_configurationMapping.ContainsKey(id))
            {
                throw new ArgumentException($"{nameof(id)} was not found");
            }

            foreach (var adapter in adapters.Where(newAdapter => !_configurationMapping[id].Contains(newAdapter)))
            {
                _configurationMapping[id].Add(adapter);
            }
        }

        public List<string> GetAdapters(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return new List<string>();
            }

            if (!_configurationMapping.ContainsKey(id))
            {
                throw new ArgumentException($"{nameof(id)} was not found");
            }

            return _configurationMapping[id];
        }

        public void Initialize()
        {
        }

        public void ResetSettingsConfiguration()
        {
            _configurationMapping.Clear();
        }

        public void LoadSettingFromUiConfig(UiConfigurationRoot configurationRoot)
        {
            if (configurationRoot.Id == null)
            {
                return;
            }

            var id = configurationRoot.Id.Value.ToString();

            if (!_configurationMapping.ContainsKey(id))
            {
                _configurationMapping.Add(id, new List<string>());
            }
            else
            {
                _configurationMapping[id].Clear();
            }

            foreach (var category in configurationRoot.Categories)
            {
                foreach (var controlModel in category.ControlModels)
                {
                    var convertedControlModel = _controlModelService.Convert(controlModel);
                    if (convertedControlModel != null)
                    {
                        var identifier = $"{convertedControlModel.Adapter}:{convertedControlModel.Identifier}";
                        if (!_configurationMapping[id].Contains(identifier))
                        {
                            _configurationMapping[id].Add(identifier);
                        }
                    }
                }
            }

            
        }
    }
}
