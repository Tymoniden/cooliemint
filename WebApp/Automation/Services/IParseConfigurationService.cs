using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebControlCenter.Automation.Services
{
    public interface IParseConfigurationService
    {
        List<Rule> ParseConfiguration(string content);
    }
}
