namespace WebControlCenter.CommandAdapter.MultiSwitch
{
    public class MultiSwitchInitializationArgument
    {
        public string Identifier { get; set; }
        
        public int StartIndex { get; set; }

        public int NumSwitches { get; set; }

        public string TopicPrefix { get; set; }
    }
}
