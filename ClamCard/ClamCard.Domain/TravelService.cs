namespace ClamCard.Domain
{
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
            var currentDailySum = GetCurrentDailySum(journey, card);
            var fare = _fareCalculationService.CalculateCost(journey, currentDailySum);

            if (fare > 0)
            {
                card.Withdraw(fare);
            }

            card.TravellingHistory.Add(new JourneyLogEntry { Journey = journey, Cost = fare });
        }

        private static double GetCurrentDailySum(Journey journey, ClamCard card)
        {
            return card.TravellingHistory.Where(x => x.Journey.End.Date.Date == journey.Start.Date.Date).Sum(x => x.Cost);
        }
    }
}