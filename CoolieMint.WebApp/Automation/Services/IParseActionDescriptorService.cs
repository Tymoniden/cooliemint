using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WebControlCenter.Automation.Services
{
    public interface IParseActionDescriptorService
    {
        List<IAutomationAction> ParseActionDescriptors(JArray array);

        IAutomationAction ParseActionDescriptor(JObject jObject);
    }
}
