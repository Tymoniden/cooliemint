using CoolieMint.WebApp.Services.Mqtt;
using Newtonsoft.Json.Linq;

namespace WebControlCenter.Services
{
    public class MqttAdapterEntry
    {
        public MqttAdapterTypes Type { get; set; }
        public JObject Arguments { get; set; }
    }
}