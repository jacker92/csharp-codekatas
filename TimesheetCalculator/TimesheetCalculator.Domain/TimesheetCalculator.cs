namespace TimesheetCalculator.Domain
{
    public class TimesheetCalculator
    {
        public TimesheetCalculationResult Calculate(TimesheetTime startTime, TimesheetTime endTime, TimesheetTimeDuration? breakDuration = null)
        {
            return new TimesheetCalculationResult (new TimesheetTimeDuration());
        }
    }
}