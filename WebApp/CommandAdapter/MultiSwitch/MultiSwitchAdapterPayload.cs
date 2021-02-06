using WebControlCenter.CommandAdapter.Enums;

namespace WebControlCenter.CommandAdapter.MultiSwitch
{
    public class MultiSwitchAdapterPayload
    {
        public int Index { get; set; }

        public PowerSocketState State { get; set; }
    }
}
