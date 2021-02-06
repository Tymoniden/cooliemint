namespace WebControlCenter.Models
{
    public class SonoffControlModel : IControlModel
    {
        public string Identifier { get; set; }
        public string Title { get; set; }
        public string Adapter { get; set; } = "Mqtt:SonoffAdapter";
        public ControlAlignment Alignment { get; set; }
        public ControlSize Size { get; set; } = ControlSize.Half;
    }
}