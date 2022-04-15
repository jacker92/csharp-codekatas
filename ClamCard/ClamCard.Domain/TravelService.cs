namespace ClamCard.Domain
{
    public class FareCalculationService
    {
        private readonly FareFactory _fareFactory;
        public FareCalculationService()
        {
            _fareFactory = new FareFactory();
        }
        public double CalculateCost(Journey journey)
        {
            var startZoneCost = _fareFactory.GetFareFor(journey.Start.Zone);
            var endZoneCost = _fareFactory.GetFareFor(journey.End.Zone);

            return Math.Max(startZoneCost.Single, endZoneCost.Single);
        }
    }
    public class TravelService
    {
        private readonly FareCalculationService _fareCalculationService;

        public TravelService()
        {
            _fareCalculationService = new FareCalculationService();
        }

        public void Travel(User user, Journey journey)
        {
            var card = user.ClamCard;
            var fare = _fareCalculationService.CalculateCost(journey);
            card.Withdraw(fare);
            card.TravellingHistory.Add(new JourneyLogEntry { Journey = journey, Cost = fare });
        }
    }
}