namespace WebControlCenter.Automation
{
    public interface IAutomationAction
    {
    }

    public sealed class MqttAction : IAutomationAction
    {
        public string Topic { get; set; }
        public string Payload { get; set; }

        public override string ToString()
        {
            return $"MqttAction: [{Topic}] {Payload}";
        }
    }

    public sealed class SmartDeviceAction : IAutomationAction
    {

    }

    public sealed class ValueStoreAction : IAutomationAction
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
