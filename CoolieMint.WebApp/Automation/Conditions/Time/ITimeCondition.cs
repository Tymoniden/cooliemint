using System;

namespace CoolieMint.WebApp.Automation.Conditions.Time
{
    public interface ITimeCondition
    {
        TimeSpan Time { get; set; }
    }
}
