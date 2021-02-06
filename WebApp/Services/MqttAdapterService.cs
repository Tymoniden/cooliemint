using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using WebControlCenter.CommandAdapter;

namespace WebControlCenter.Services
{
    public class MqttAdapterService : IMqttAdapterService
    {
        private readonly IMqttAdapterFactory _mqttAdapterFactory;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileSystemService _fileSystemService;
        private readonly IJsonSerializerService _jsonSerializerService;

        public MqttAdapterService(IMqttAdapterFactory mqttAdapterFactory,IWebHostEnvironment webHostEnvironment, IFileSystemService fileSystemService, IJsonSerializerService jsonSerializerService)
        {
            _mqttAdapterFactory = mqttAdapterFactory;
            _webHostEnvironment = webHostEnvironment;
            _fileSystemService = fileSystemService;
            _jsonSerializerService = jsonSerializerService;
        }

        public List<IMqttAdapter> ReadConfiguration()
        {
            var adapters = new List<IMqttAdapter>();
            var adapterEntries = ReadConfigFile();
            foreach (var adapterEntry in adapterEntries)
            {
                var adapter = _mqttAdapterFactory.CreateMqttAdapter(adapterEntry);
                if(adapter != null)
                {
                    adapters.Add(adapter);
                }
            }

            return adapters;
        }

        MqttAdapterEntry[] ReadConfigFile()
        {
            var adapterConfigFile = Path.Combine(_webHostEnvironment.ContentRootPath, "configuration" , "mqttAdapter.json");
            var content = _fileSystemService.ReadFileAsString(adapterConfigFile);
            return _jsonSerializerService.Deserialize<MqttAdapterEntry[]>(content);
        }
    }
}