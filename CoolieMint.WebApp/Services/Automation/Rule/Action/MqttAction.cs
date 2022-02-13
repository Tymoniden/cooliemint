namespace CoolieMint.WebApp.Services.Automation.Rule.Action
{
    public sealed class MqttAction : IAutomationAction
    {
        public string Topic { get; set; }
        public string Payload { get; set; }

        public override string ToString()
        {
            return $"MqttAction: [{Topic}] {Payload}";
        }
    }
}
