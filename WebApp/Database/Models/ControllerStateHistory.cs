using System;

namespace WebControlCenter.Database.Models
{
    public class ControllerStateHistory
    {
        public long Id { get; set; }
        public ControllerStateInformation ControllerStateInformation { get; set; }
        public long ControllerStateInformationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
