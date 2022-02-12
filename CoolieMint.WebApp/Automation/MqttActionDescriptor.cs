namespace WebControlCenter.Automation
{
    public class MqttActionDescriptor : IAutomationAction
    {
        public string Topic { get; set; }
        public string Body { get; set; }
        public bool IsRetained { get; set; }
        public ActionDescriptorType Type { get; set; } = ActionDescriptorType.MQTT;
    }

}
