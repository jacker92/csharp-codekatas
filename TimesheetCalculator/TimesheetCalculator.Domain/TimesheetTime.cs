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
    }
}