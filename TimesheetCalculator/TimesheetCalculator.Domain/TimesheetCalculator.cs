namespace TimesheetCalculator.Domain
{
    public class TimesheetCalculator
    {
        public TimesheetCalculationResult Calculate(TimesheetTime startTime, TimesheetTime endTime, TimesheetTime? breakDuration = null)
        {
            if (startTime is null)
            {
                throw new ArgumentNullException(nameof(startTime));
            }

            if (endTime is null)
            {
                throw new ArgumentNullException(nameof(endTime));
            }

            var minuteDifference = Math.Abs(endTime.TotalTimeInMinutes - startTime.TotalTimeInMinutes);

            var hours = minuteDifference / 60;
            var minutes = minuteDifference % 60;

            if (hours < 0) hours += 24;

            if (breakDuration != null && minuteDifference < breakDuration.TotalTimeInMinutes)
            {
                throw new ArgumentException("Break cannot be longer that the time difference between start and end time.");
            }

            return new TimesheetCalculationResult(new TimesheetTime(hours, minutes));
        }
    }
}