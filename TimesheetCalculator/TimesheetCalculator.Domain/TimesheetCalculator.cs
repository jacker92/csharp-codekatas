namespace TimesheetCalculator.Domain
{
    public class TimesheetCalculator
    {
        public TimesheetCalculationResult Calculate(TimesheetTime startTime, TimesheetTime endTime, TimesheetTimeDuration? breakDuration = null)
        {
            if (startTime is null)
            {
                throw new ArgumentNullException(nameof(startTime));
            }

            if (endTime is null)
            {
                throw new ArgumentNullException(nameof(endTime));
            }

            var hours = endTime.Hours - startTime.Hours;
            var minutes = endTime.Minutes - startTime.Minutes;

            if (minutes < 0)
            {
                hours--;
                minutes += 60;
            }

            if (hours < 0)
            {
                hours += 24;
            }

            return new TimesheetCalculationResult(new TimesheetTimeDuration(hours, minutes));
        }
    }
}