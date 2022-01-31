using System;

namespace CoolieMint.WebApp.Services
{
    public interface IDateTimeProvider
    {
        DateTime AsUtc(DateTime dateTime);
        DateTime Now();
        DateTime UtcNow();
    }
}