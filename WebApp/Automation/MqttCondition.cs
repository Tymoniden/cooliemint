﻿namespace WebControlCenter.Automation
{
    public class MqttCondition : IConditionParameter
    {
        public MqttComparer Comparer { get; set; }
        public string Topic { get; set; }
        public string Body { get; set; }
    }

}
