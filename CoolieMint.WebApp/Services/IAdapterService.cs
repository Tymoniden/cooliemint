using System;
using System.Collections.Generic;
using WebControlCenter.Controllers;

namespace WebControlCenter.Services
{
    public interface IAdapterService
    {
        List<AdapterStatusMessage> GetUpdates(DateTime referenceTimestamp);

        List<AdapterStatusMessage> GetUpdates(DateTime referenceTimestamp, List<string> adapters);

        void SendMessage(CommandAdapterMessage commandAdapterMessage);
    }
}