using Newtonsoft.Json.Linq;
using System;

namespace WebControlCenter.Services
{
    public class UiConfigurationCategory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Symbol { get; set; }
        public JObject[] ControlModels { get; set; }
    }
}