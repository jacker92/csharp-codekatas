namespace TimesheetCalculator.Domain
{
    public class TimesheetTimeDuration
    {
        public TimesheetTimeDuration(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }

        public int Hours { get; }
        public int Minutes { get; }

        public override string? ToString()
        {
            return $"{Hours:00}:{Minutes:00}";
        }
    }
}