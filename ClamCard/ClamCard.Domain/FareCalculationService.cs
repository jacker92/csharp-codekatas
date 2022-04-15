namespace ClamCard.Domain
{
    public class FareCalculationService
    {
        private readonly FareFactory _fareFactory;
        public FareCalculationService()
        {
            _fareFactory = new FareFactory();
        }
        public double CalculateCost(Journey journey, ClamCard clamCard)
        {
            var startZoneCost = _fareFactory.GetFareFor(journey.Start.Zone);
            var endZoneCost = _fareFactory.GetFareFor(journey.End.Zone);

            var max = Math.Max(startZoneCost.Single, endZoneCost.Single);
            var dailyMax = Math.Max(startZoneCost.Day, endZoneCost.Day);

            return CalculateJourneyCost(max, dailyMax, journey, clamCard);
        }

        private double CalculateJourneyCost(double max, double dailyMax, Journey journey, ClamCard clamCard)
        {
            var currentDailySum = GetCurrentDailySum(journey, clamCard);
            var currentWeeklySum = GetCurrentWeeklySum(journey, clamCard);

            bool hasExistingJourneys = HasExistingJourneys(clamCard);
            bool currentJourneyOnSameDateAsLastJourney = CurrentJourneyIsOnSameDateAsLastJourney(journey, clamCard);

            if (hasExistingJourneys && !currentJourneyOnSameDateAsLastJourney)
            {
                return max;
            }

            if (currentJourneyOnSameDateAsLastJourney && currentDailySum + max > dailyMax)
            {
                return dailyMax - currentDailySum;
            }

            return max;
        }

        private static double GetCurrentDailySum(Journey journey, ClamCard card)
        {
            return card.TravellingHistory.Where(x => x.Journey.End.Date.Date == journey.Start.Date.Date).Sum(x => x.Cost);
        }

        private static double GetCurrentWeeklySum(Journey journey, ClamCard card)
        {
            return card.TravellingHistory.Where(x => DateTimeHelpers.Between(x.Journey.End.Date.Date, journey.Start.Date.Date.AddDays(-7), x.Journey.End.Date.Date)).Sum(x => x.Cost);
        }

        private static bool HasExistingJourneys(ClamCard clamCard)
        {
            return clamCard.TravellingHistory.Count() > 0;
        }
        private static bool CurrentJourneyIsOnSameDateAsLastJourney(Journey journey, ClamCard clamCard)
        {
            return clamCard.TravellingHistory.LastOrDefault()?.Journey?.End?.Date.Date == journey.Start.Date.Date;
        }
    }
}