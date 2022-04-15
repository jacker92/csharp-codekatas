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

            var fare = _fareCalculationService.CalculateCost(journey, card);

            if (fare > 0)
            {
                card.Withdraw(fare);
            }

            card.TravellingHistory.Add(new JourneyLogEntry { Journey = journey, Cost = fare });
        }
    }
}