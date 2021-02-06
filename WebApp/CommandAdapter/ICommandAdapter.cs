using System;
using System.Collections.Generic;
using WebControlCenter.Controllers;
using WebControlCenter.Services;

namespace WebControlCenter.CommandAdapter
{
    public interface ICommandAdapter
    {
        List<AdapterStatusMessage> GetAdapterStatusMessages(DateTime timestamp);

        List<AdapterStatusMessage> GetAdapterStatusMessages(DateTime timestamp, List<string> adapters);

        void SendMessage(CommandAdapterMessage commandAdapterMessage);
    }
}