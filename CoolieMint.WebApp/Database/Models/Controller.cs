using System.Collections.Generic;

namespace WebControlCenter.Database.Models
{
    public class Controller
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Identifier { get; set; }
        public string InitializationArguments { get; set; }
        //public List<ControllerStatusSegment> ControllerStatusSegments { get; set; } = new List<ControllerStatusSegment>();
        public List<ControllerStateInformation> ControllerStateInformations { get; set; } = new List<ControllerStateInformation>();
        //public List<ControllerStateHistory> ControllerStateHistories { get; set; } = new List<ControllerStateHistory>();
    }
}
