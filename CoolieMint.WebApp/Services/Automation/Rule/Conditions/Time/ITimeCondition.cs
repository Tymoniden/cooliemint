using System;

namespace CoolieMint.WebApp.Services.Automation.Rule.Conditions.Time
{
    public interface ITimeCondition
    {
        TimeSpan Time { get; set; }
    }
}
