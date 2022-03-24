namespace TimesheetCalculator.Domain
{
    public class TimesheetCalculationResult
    {
        public TimesheetCalculationResult(TimesheetTimeDuration duration)
        {
            Duration = duration;
        }

        public TimesheetTimeDuration Duration { get; }
    }
}