namespace TimesheetCalculator.Domain
{
    public class TimesheetTimeParsingException : Exception
    {
        public TimesheetTimeParsingException(string? message) : base(message)
        {
        }
    }
}