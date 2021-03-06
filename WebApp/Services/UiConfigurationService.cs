﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing.Template;
using Newtonsoft.Json.Linq;

namespace WebControlCenter.Services
{
    public class UiConfigurationService : IUiConfigurationService
    {
        private static Dictionary<string,UiConfigurationRoot> _configurations = new Dictionary<string, UiConfigurationRoot>();
        private string BaseFolder = $"configuration{Path.DirectorySeparatorChar}ui";
        private readonly IFileSystemService _fileSystemService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IJsonSerializerService _jsonSerializerService;
        private readonly IAdapterSettingService _adapterSettingService;

        public UiConfigurationService(IFileSystemService fileSystemService, IWebHostEnvironment hostingEnvironment, IJsonSerializerService jsonSerializerService, IAdapterSettingService adapterSettingService)
        {
            _fileSystemService = fileSystemService;
            _hostingEnvironment = hostingEnvironment;
            _jsonSerializerService = jsonSerializerService;
            _adapterSettingService = adapterSettingService;
        }

        public void ReadAllConfigurationFiles()
        {
            var configFolder = Path.Combine(_hostingEnvironment.ContentRootPath, BaseFolder);

            foreach (var config in _fileSystemService.ReadFilesInFolder(configFolder))
            {
                var fileContent = _fileSystemService.ReadFileAsString(config);
                var root = _jsonSerializerService.Deserialize<UiConfigurationRoot>(fileContent);
                if (root != null)
                {
                    root.Id ??= Guid.NewGuid();

                    _configurations[root.Name] = root;
                    _adapterSettingService.LoadSettingFromUiConfig(root);
                }
            }
        }

        public Dictionary<string, Guid> GetConfiguredUsers()
        {
            return _configurations.ToDictionary(config => config.Key, config => config.Value.Id.Value);
        }

        public UiConfigurationRoot GetConfiguration(string name) => _configurations[name];
    }

    public class UiConfigurationRoot{
        public string Name { get; set; }
        public string Symbol { get; set; }
        public Guid? Id { get; set; }
        public UiConfigurationCategory[] Categories { get; set; }
    }

    public class UiConfigurationCategory{
        public string Name { get; set; }
        public string Symbol { get; set; }
        public JObject[] ControlModels { get; set; }
    }
}