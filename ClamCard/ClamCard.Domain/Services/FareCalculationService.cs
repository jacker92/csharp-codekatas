using ClamCard.Domain.Helpers;
using ClamCard.Domain.Models;
using ClamCard.Domain.Models.Fares;

namespace ClamCard.Domain.Services
{
    public class FareCalculationService
    {
        private readonly JourneyFareCalculationService _journeyFareCalculationService;

        public FareCalculationService()
        {
            _journeyFareCalculationService = new JourneyFareCalculationService();
        }

        public double CalculateCost(Journey journey, Models.ClamCard clamCard)
        {
            var journeyFare = _journeyFareCalculationService.Calculate(journey);

            var currentMontlySum = GetCurrentMonthlySum(journey, clamCard);
            if (currentMontlySum + journeyFare.MaxSingleCost > journeyFare.MonthlyMax)
            {
                return journeyFare.MonthlyMax - currentMontlySum;
            }

            var currentWeeklySum = GetCurrentWeeklySum(journey, clamCard);
            if (currentWeeklySum + journeyFare.MaxSingleCost > journeyFare.WeeklyMax)
            {
                return journeyFare.WeeklyMax - currentWeeklySum;
            }

            var currentDailySum = GetCurrentDailySum(journey, clamCard);
            if (currentDailySum + journeyFare.MaxSingleCost > journeyFare.DailyMax)
            {
                return journeyFare.DailyMax - currentDailySum;
            }

            return journeyFare.MaxSingleCost;
        }

        private static double GetCurrentMonthlySum(Journey journey, Models.ClamCard clamCard)
        {
            return clamCard.TravellingHistory.Where(x => DateTimeHelpers.BetweenInclusive(x.Journey.End.Date.Date, journey.Start.Date.Date.AddMonths(-1), x.Journey.End.Date.Date)).Sum(x => x.Cost);
        }

        private static double GetCurrentDailySum(Journey journey, Models.ClamCard card)
        {
            return card.TravellingHistory.Where(x => x.Journey.End.Date.Date == journey.Start.Date.Date).Sum(x => x.Cost);
        }

        private static double GetCurrentWeeklySum(Journey journey, Models.ClamCard card)
        {
            return card.TravellingHistory.Where(x => DateTimeHelpers.BetweenInclusive(x.Journey.End.Date.Date, journey.Start.Date.Date.AddDays(-7), x.Journey.End.Date.Date)).Sum(x => x.Cost);
        }
    }
}