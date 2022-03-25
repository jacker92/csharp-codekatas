namespace PokerHands.Domain
{
    public class PokerHand
    {
        public PokerHand(IEnumerable<PlayingCard> cards)
        {
            if (cards is null)
            {
                throw new ArgumentNullException(nameof(cards));
            }

            if (cards.Count() != 5)
            {
                throw new ArgumentException("Pokerhand must have five cards.");
            }

            Cards = cards;
        }

        public IEnumerable<PlayingCard> Cards { get; }

        public PokerHandRank Rank => CalculateRank();

        private PokerHandRank CalculateRank()
        {
            var max = Cards.MaxBy(x => x.Value)!;

            if (max.Value == 8) return PokerHandRank.HighestCardEight;

            return PokerHandRank.HighestCardSeven;
        }
    }
}