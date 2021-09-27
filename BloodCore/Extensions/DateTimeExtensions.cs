using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool PassedLongerThan(this DateTime date, DateTime then, TimeSpan time)
        {
            return (then - date).CompareTo(time) > 0;
        }

        public static TimeSpan DifferYears(this DateTime date, int years)
        {
            return date - DateTime.Now.AddYears(-years);
        }
    }
}
