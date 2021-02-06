using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WebControlCenter.Automation.Services
{
    public interface IParseActionDescriptorService
    {
        List<IActionDescriptor> ParseActionDescriptors(JArray array);

        IActionDescriptor ParseActionDescriptor(JObject jObject);
    }
}
