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

            var minuteDifference = Math.Abs((endTime.Hours * 60 + endTime.Minutes) - (startTime.Hours * 60 + startTime.Minutes));

            var hours = minuteDifference / 60;
            var minutes = minuteDifference % 60;

            if (hours < 0) hours += 24;

            return new TimesheetCalculationResult(new TimesheetTimeDuration(hours, minutes));
        }
    }
}