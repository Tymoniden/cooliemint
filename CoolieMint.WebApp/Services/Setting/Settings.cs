using System.Collections.Generic;

namespace WebControlCenter.Services.Setting
{
    public class Settings
    {
        public string LogDirectory { get; set; } = "Logs";

        public string MqttServerAdress { get; set; } = "127.0.0.0";

        public string MqttClientId { get; set; } = "default";

        public string MqttClientUsername { get; set; }

        public string MqttClientPassword { get; set; }

        public List<PushoverAccount> PushOverAccounts { get; set; } = new List<PushoverAccount>();
    }
}
