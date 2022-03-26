using System.Linq;

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

            Cards = cards;
        }

        public IEnumerable<PlayingCard> Cards { get; }

        public PokerHandRank Rank => CalculateRank();

        public static bool operator <(PokerHand a, PokerHand b)
        {
            return !(a.Rank > b.Rank);
        }

        public static bool operator >(PokerHand a, PokerHand b)
        {
            if (a == b)
            {
                return false;
            }

            if (a.Cards.Count() == 0)
            {
                return false;
            }

            if (a.Rank == b.Rank)
            {
                if (a.Cards.Count() == 5)
                {
                    return !CompareTwoHandsWithSameRank(a, b);
                }

                return new PokerHand(a.Cards.Take(a.Cards.Count() - 1)) <
                       new PokerHand(b.Cards.Take(b.Cards.Count() - 1));
            }

            return a.Rank > b.Rank;
        }

        public static bool operator ==(PokerHand a, PokerHand b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(PokerHand a, PokerHand b)
        {
            return !a.Equals(b);
        }

        private PokerHandRank CalculateRank()
        {
            var groupedCards = PokerHandHelper.GroupCards(Cards);

            var hasThreeOfAKind = groupedCards.Any(x => x.Count == 3);
            var amountOfPairs = groupedCards.Where(x => x.Count == 2).Count();

            var straightSequence = Enumerable.Range(Cards.MinBy(x => x.Value)!.Value, 5);
            var areInSequence = Cards.Select(x => x.Value).SequenceEqual(straightSequence);

            if (areInSequence)
            {
                return PokerHandRank.Straight;
            }
            else if (hasThreeOfAKind)
            {
                return PokerHandRank.ThreeOfAKind;
            }
            else if (amountOfPairs == 2)
            {
                return PokerHandRank.TwoPairs;
            }
            else if (amountOfPairs == 1)
            {
                return PokerHandRank.OnePair;
            }

            if (Cards.Any(x => x.Value == 1)) return PokerHandRank.HighestCardAce;

            var max = Cards.MaxBy(x => x.Value)!;

            if (max.Value == 7) return PokerHandRank.HighestCardSeven;
            if (max.Value == 8) return PokerHandRank.HighestCardEight;
            if (max.Value == 9) return PokerHandRank.HighestCardNine;
            if (max.Value == 10) return PokerHandRank.HighestCardTen;
            if (max.Value == 11) return PokerHandRank.HighestCardJack;
            if (max.Value == 12) return PokerHandRank.HighestCardQueen;
            if (max.Value == 13) return PokerHandRank.HighestCardKing;

            throw new Exception();
        }

        private static bool CompareTwoHandsWithSameRank(PokerHand a, PokerHand b)
        {
            var groupedCards = PokerHandHelper.GroupCards(a.Cards);
            var secondGroupedCards = PokerHandHelper.GroupCards(b.Cards);

            var pairs = groupedCards.Where(x => x.Count == 2);
            var secondPairs = secondGroupedCards.Where(x => x.Count == 2);

            switch (a.Rank)
            {
                case PokerHandRank.OnePair:
                    return pairs.First().Key < secondPairs.First().Key;

            }

            throw new Exception();
        }

        public override bool Equals(object? obj)
        {
            return obj is PokerHand hand &&
                   EqualityComparer<IEnumerable<PlayingCard>>.Default.Equals(Cards, hand.Cards) &&
                   Rank == hand.Rank;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Cards, Rank);
        }
    }
}