namespace TimesheetCalculator.Domain
{
    public class TimesheetTime
    {
        public TimesheetTime(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }

        public TimesheetTime() : this(0, 0) { }

        public int Hours { get; }
        public int Minutes { get; }
        public int TotalTimeInMinutes => Hours * 60 + Minutes;

        public override string? ToString()
        {
            return $"{Hours:00}:{Minutes:00}";
        }
    }
}