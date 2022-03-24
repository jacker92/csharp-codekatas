namespace TimesheetCalculator.Domain
{
    public class TimesheetCalculationException : Exception
    {
        public TimesheetCalculationException(string message) : base(message)
        {
        }
    }
}