using CoolieMint.WebApp.Services.Automation.Rule.Action;

namespace CoolieMint.WebApp.Dtos.Automation
{
    public class MqttActionDto : IAutomationActionDto
    {
        public string Topic { get; set; }
        public string Payload { get; set; }
    }
}
