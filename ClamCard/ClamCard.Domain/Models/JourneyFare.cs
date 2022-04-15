namespace ClamCard.Domain.Models
{
    public class JourneyFare
    {
        public double MaxSingleCost { get; set; }
        public double DailyMax { get; set; }
        public double WeeklyMax { get; set; }
        public double MonthlyMax { get; internal set; }
    }
}