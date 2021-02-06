using System.ComponentModel.DataAnnotations;

namespace WebControlCenter.Database.Entities
{
    public class ControllerStateInformation
    {
        [Key] 
        public long Id { get; set; }
        public long ControllerId { get; set; }
        public Controller Controller { get; set; }
        public string State { get; set; }
        public double PowerConsumption { get; set; }
    }
}