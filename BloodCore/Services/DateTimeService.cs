using System;

namespace BloodCore.Services
{
    [Injectable]
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now() => DateTime.Now;
    }
}
