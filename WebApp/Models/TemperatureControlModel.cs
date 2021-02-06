namespace WebControlCenter.Models
{
    public class TemperatureControlModel : IControlModel
    {
        public string Identifier { get; set; }
        public string Title { get; set; }
        public string Adapter { get; set; } = "Mqtt:TemperatureAdapter";
        public double MinValue { get; set; } = 0.0;
        public double MaxValue { get; set; } = 100.0;
        public ControlAlignment Alignment { get; set; }
        public ControlSize Size { get; set; } = ControlSize.Half;
    }
}