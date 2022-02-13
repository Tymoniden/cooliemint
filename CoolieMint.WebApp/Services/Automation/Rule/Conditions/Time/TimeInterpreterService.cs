using CoolieMint.WebApp.Services;
using System;

namespace CoolieMint.WebApp.Services.Automation.Rule.Conditions.Time
{
    public class TimeInterpreterService : ITimeInterpreterService
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public TimeInterpreterService(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public bool IsTrue(ICondition condition)
        {
            if (condition is ITimeCondition timeCondition)
            {
                var referenceDateTime = _dateTimeProvider
                    .Now()
                    .Date
                    .AddHours(timeCondition.Time.Hours)
                    .AddMinutes(timeCondition.Time.Minutes)
                    .AddSeconds(timeCondition.Time.Seconds);

                if (condition is TimeEarlierCondition timeEarlierCondition)
                {
                    return _dateTimeProvider.Now() < referenceDateTime;
                }

                if (condition is TimeLaterCondition timeLaterCondition)
                {
                    return _dateTimeProvider.Now() > referenceDateTime;
                }
            }

            throw new ArgumentException(nameof(condition));
        }
    }
}
