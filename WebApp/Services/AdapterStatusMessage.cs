using System;

namespace WebControlCenter.Services
{
    public class AdapterStatusMessage
    {
        public string Identifier { get; set; }

        public string AdapterType { get; set; }
        
        public DateTime Timestamp { get; set; }

        // Object that will be cast to json
        public object Payload { get; set; }
    }
}
