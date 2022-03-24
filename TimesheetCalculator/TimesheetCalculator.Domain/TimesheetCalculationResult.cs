namespace TimesheetCalculator.Domain
{
    public class TimesheetCalculationResult
    {
        public TimesheetCalculationResult(TimesheetTime duration)
        {
            Duration = duration;
        }

        public TimesheetTime Duration { get; }
    }
}