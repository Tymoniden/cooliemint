using System;

namespace WebControlCenter.Repository
{
    public class MqttValueContract
    {
        public string Topic { get; set; }

        public object Payload { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}