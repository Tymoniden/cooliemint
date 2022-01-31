using System;

namespace CoolieMint.WebApp.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }

        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }

        public DateTime AsUtc(DateTime dateTime)
        {
            return dateTime.ToUniversalTime();
        }
    }
}
