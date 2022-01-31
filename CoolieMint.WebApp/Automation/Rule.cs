using System.Collections.Generic;

namespace WebControlCenter.Automation
{
    public class Rule
    {
        public string Name { get; set; }
        public Condition Condition { get; set; }
        public List<IActionDescriptor> OnTrue { get; set; } = new List<IActionDescriptor>();
        public List<IActionDescriptor> OnFalse { get; set; } = new List<IActionDescriptor>();
    }

}
