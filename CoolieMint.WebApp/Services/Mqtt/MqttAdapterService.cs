using System.Collections.Generic;
using System.IO;
using CoolieMint.WebApp.Services.FileSystem;
using Microsoft.AspNetCore.Hosting;
using WebControlCenter.CommandAdapter;
using WebControlCenter.Services;

namespace CoolieMint.WebApp.Services.Mqtt
{
    public class MqttAdapterService : IMqttAdapterService
    {
        private readonly IMqttAdapterFactory _mqttAdapterFactory;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileSystemService _fileSystemService;
        private readonly IJsonSerializerService _jsonSerializerService;
        List<MqttAdapterEntry> _adapterEntries = new List<MqttAdapterEntry>();

        public MqttAdapterService(IMqttAdapterFactory mqttAdapterFactory, IWebHostEnvironment webHostEnvironment, IFileSystemService fileSystemService, IJsonSerializerService jsonSerializerService)
        {
            _mqttAdapterFactory = mqttAdapterFactory;
            _webHostEnvironment = webHostEnvironment;
            _fileSystemService = fileSystemService;
            _jsonSerializerService = jsonSerializerService;
        }

        public List<IMqttAdapter> ReadConfiguration()
        {
            var adapters = new List<IMqttAdapter>();
            foreach (var adapterEntry in ReadConfigFile())
            {
                var adapter = _mqttAdapterFactory.CreateMqttAdapter(adapterEntry);
                if (adapter != null)
                {
                    adapters.Add(adapter);
                }

                _adapterEntries.Add(adapterEntry);
            }

            return adapters;
        }

        public List<MqttAdapterEntry> GetAdapterEntries() => _adapterEntries;

        MqttAdapterEntry[] ReadConfigFile()
        {
            var adapterConfigFile = Path.Combine(_webHostEnvironment.ContentRootPath, "configuration", "mqttAdapter.json");
            var content = _fileSystemService.ReadFileAsString(adapterConfigFile);
            return _jsonSerializerService.Deserialize<MqttAdapterEntry[]>(content);
        }
    }
}