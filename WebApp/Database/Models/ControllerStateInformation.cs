namespace WebControlCenter.Database.Models
{
    public class ControllerStateInformation
    {
        public long Id { get; set; }
        public long ControllerId { get; set; }
        public Controller Controller { get; set; }
        public string State { get; set; }
        public double PowerConsumption { get; set; }
    }
}
