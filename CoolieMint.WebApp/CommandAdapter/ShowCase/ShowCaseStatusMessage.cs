using System;

namespace WebControlCenter.CommandAdapter.ShowCase
{
    public class ShowCaseStatusMessage : IAdapterStatusMessage
    {
        public DateTime TimeStamp { get; set; }

        public object StateMapping { get; set; }
    }
}
