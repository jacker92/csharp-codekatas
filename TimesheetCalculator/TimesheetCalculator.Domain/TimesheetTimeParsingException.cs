namespace TimesheetCalculator.Domain
{
    public class TimesheetTimeParsingException : Exception
    {
        public TimesheetTimeParsingException(string? message = "Invalid format.") : base(message)
        {
        }
    }
}