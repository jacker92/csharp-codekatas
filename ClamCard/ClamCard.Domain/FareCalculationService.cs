namespace ClamCard.Domain
{
    public class FareCalculationService
    {
        private readonly FareFactory _fareFactory;
        public FareCalculationService()
        {
            _fareFactory = new FareFactory();
        }
        public double CalculateCost(Journey journey, double currentDailySum)
        {
            var startZoneCost = _fareFactory.GetFareFor(journey.Start.Zone);
            var endZoneCost = _fareFactory.GetFareFor(journey.End.Zone);

            var max = Math.Max(startZoneCost.Single, endZoneCost.Single);
            var dailyMax = Math.Max(startZoneCost.Day, endZoneCost.Day);

            return CalculateJourneyCost(currentDailySum, max, dailyMax);
        }

        private static double CalculateJourneyCost(double currentDailySum, double max, double dailyMax)
        {
            if (currentDailySum + max > dailyMax)
            {
                return dailyMax - currentDailySum;
            }

            return max;
        }
    }
}