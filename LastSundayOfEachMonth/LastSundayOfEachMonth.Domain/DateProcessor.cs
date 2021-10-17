using System;
using System.Collections.Generic;

namespace LastSundayOfEachMonth.Domain
{
    public class DateProcessor
    {
        public IList<DateTimeOffset> LastSunday(int year)
        {
            if (year < 1900 || year > 9999)
            {
                throw new ArgumentOutOfRangeException(nameof(year));
            }

            var sundays = new List<DateTimeOffset>();

            for (int i = 1; i <= 12; i++)
            {
                var date = new DateTimeOffset(year, i, DateTime.DaysInMonth(year, i), 0, 0, 0, TimeSpan.Zero);

                if (date.DayOfWeek != DayOfWeek.Sunday)
                {
                    date = new DateTimeOffset(year, i, date.Day - (int)date.DayOfWeek, 0, 0, 0, TimeSpan.Zero);
                }

                sundays.Add(date);
            }

            return sundays;
        }
    }
}
