using System;
using System.ComponentModel.DataAnnotations;

namespace WebControlCenter.Database.Entities
{
    public class ControllerStateHistory
    {
        [Key] 
        public long Id { get; set; }
        public ControllerStateInformation ControllerStateInformation { get; set; }
        public long ControllerStateInformationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}