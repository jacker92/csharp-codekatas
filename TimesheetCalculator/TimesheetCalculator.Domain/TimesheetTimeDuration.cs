namespace TimesheetCalculator.Domain
{
    public class TimesheetTimeDuration
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }

        public override string? ToString()
        {
            return $"{Hours:00}:{Minutes:00}";
        }
    }
}