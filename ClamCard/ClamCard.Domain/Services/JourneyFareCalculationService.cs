using ClamCard.Domain.Factories;
using ClamCard.Domain.Models;

namespace ClamCard.Domain.Services
{
    public class JourneyFareCalculationService
    {
        private readonly FareFactory _fareFactory;

        public JourneyFareCalculationService()
        {
            _fareFactory = new FareFactory();
        }

        public JourneyFare Calculate(Journey journey)
        {
            var first = journey.Start.Zone;
            var second = journey.End.Zone;

            var startZoneCost = _fareFactory.GetFareFor(first);
            var endZoneCost = _fareFactory.GetFareFor(second);

            return new JourneyFare
            {
                MaxSingleCost = Math.Max(startZoneCost.Single, endZoneCost.Single),
                DailyMax = Math.Max(startZoneCost.Day, endZoneCost.Day),
                WeeklyMax = Math.Max(startZoneCost.Week, endZoneCost.Week),
                MonthlyMax = Math.Max(startZoneCost.Month, endZoneCost.Month),
                ReturnMax = Math.Max(startZoneCost.SingleReturn, endZoneCost.SingleReturn)
            };
        }
    }
}