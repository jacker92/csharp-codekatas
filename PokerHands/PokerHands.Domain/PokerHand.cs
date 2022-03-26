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

        internal bool HasFullHouse => HasThreeOfAKind && AmountOfPairs == 1;
        internal bool HasFlush => PokerHandHelper.HasFlush(Cards);
        internal bool HasAce => Cards.Any(x => x.Value == 1);
        internal bool HasHighestStraight => PokerHandHelper.HasHighestStraightSequence(Cards);
        internal int HighestCardValue => Cards.MaxBy(x => x.Value)!.Value;
        internal IEnumerable<PokerHandHelper.GroupedCard> GroupedCards => PokerHandHelper.GroupCards(Cards);
        internal bool HasThreeOfAKind => GroupedCards.Any(x => x.Count == 3);
        internal int AmountOfPairs => GroupedCards.Where(x => x.Count == 2).Count();
        internal bool HasStraight => PokerHandHelper.HasStraight(Cards);
        internal bool HasThreeOfAKindAces => ThreeOfAKindKey == 1;
        internal int? ThreeOfAKindKey => GroupedCards.FirstOrDefault(x => x.Count == 3)?.Key;
        internal IEnumerable<PlayingCard> WithoutThreeOfAKindCards => GroupedCards.Where(x => x.Count != 3).SelectMany(x => x.Values);
        internal IEnumerable<PlayingCard> WithoutPairCards => GroupedCards.Where(x => x.Count != 2).SelectMany(x => x.Values);
        internal IEnumerable<PlayingCard> Pairs => GroupedCards.Where(x => x.Count == 2).SelectMany(x => x.Values);
        internal bool HasPairOfAces => Pairs.Any(x => x.Value == 1);
        internal int HigherPairKey => Pairs.MaxBy(x => x.Value)!.Value;
        internal int LowerPairKey => Pairs.MinBy(x => x.Value)!.Value;
        internal IEnumerable<PlayingCard> WithoutCurrentHighestCard => Cards.OrderByDescending(x => x.Value).Take(Cards.Count() - 1);

        public static bool operator <(PokerHand a, PokerHand b)
        {
            var comparingResult = CompareTo(a, b);
            return comparingResult == -1;
        }

        public static bool operator >(PokerHand a, PokerHand b)
        {
            var comparingResult = CompareTo(a, b);
            return comparingResult == 1;
        }

        public static int CompareTo(PokerHand a, PokerHand b)
        {
            if (a.Cards.Count() == 0)
            {
                return 0;
            }

            if (a.Rank == b.Rank)
            {
                if (a.Cards.Count() == 5)
                {
                    return CompareTwoHandsWithSameRank(a, b);
                }

                return CompareTo(new PokerHand(a.Cards.Take(a.Cards.Count() - 1)),
                       new PokerHand(b.Cards.Take(b.Cards.Count() - 1)));
            }

            return a.Rank > b.Rank ? 1 : -1;
        }

        public static bool operator ==(PokerHand a, PokerHand b)
        {
            return CompareTo(a, b) == 0;
        }

        public static bool operator !=(PokerHand a, PokerHand b)
        {
            return !(a == b);
        }

        private PokerHandRank CalculateRank()
        {
            if (HasFullHouse)
            {
                return PokerHandRank.FullHouse;
            }
            else if (HasFlush)
            {
                return PokerHandRank.Flush;
            }
            else if (HasStraight)
            {
                return PokerHandRank.Straight;
            }
            else if (HasThreeOfAKind)
            {
                return PokerHandRank.ThreeOfAKind;
            }
            else if (AmountOfPairs == 2)
            {
                return PokerHandRank.TwoPairs;
            }
            else if (AmountOfPairs == 1)
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

        private static int CompareTwoHandsWithSameRank(PokerHand a, PokerHand b)
        {
            switch (a.Rank)
            {
                case PokerHandRank.FullHouse:
                    return CompareFullHouse(a, b);
                case PokerHandRank.Flush:
                    return CompareFlush(a, b);
                case PokerHandRank.Straight:
                    return CompareStraight(a, b) ? -1 : 1;
                case PokerHandRank.ThreeOfAKind:
                    return CompareThreeOfAKind(a, b) ? -1 : 1;
                case PokerHandRank.TwoPairs:
                    return CompareTwoPairs(a, b) ? -1 : 1;
                case PokerHandRank.OnePair:
                    return CompareOnePair(a, b);

            }

            throw new Exception();
        }

        private static int CompareFullHouse(PokerHand firstInfo, PokerHand secondInfo)
        {
            if (!firstInfo.HasThreeOfAKindAces && secondInfo.HasThreeOfAKindAces)
            {
                return -1;
            }

            if (firstInfo.HasThreeOfAKindAces && !secondInfo.HasThreeOfAKindAces)
            {
                return 1;
            }

            if (firstInfo.ThreeOfAKindKey == secondInfo.ThreeOfAKindKey)
            {
                var firstPokerHandInfo = new PokerHand(firstInfo.WithoutThreeOfAKindCards);
                var secondPokerHandInfo = new PokerHand(secondInfo.WithoutThreeOfAKindCards);

                return CompareOnePair(firstPokerHandInfo, secondPokerHandInfo);
            }

            return firstInfo.ThreeOfAKindKey < secondInfo.ThreeOfAKindKey ? 1 : -1;
        }

        private static int CompareFlush(PokerHand firstInfo, PokerHand secondInfo)
        {
            if (!firstInfo.HasAce && secondInfo.HasAce)
            {
                return -1;
            }

            if (firstInfo.HasAce && !secondInfo.HasAce)
            {
                return 1;
            }

            if (firstInfo.HighestCardValue != secondInfo.HighestCardValue)
            {
                return firstInfo.HighestCardValue < secondInfo.HighestCardValue ? -1 : 1;
            }

            return CompareTo(new PokerHand(firstInfo.WithoutCurrentHighestCard), new PokerHand(secondInfo.WithoutCurrentHighestCard));
        }

        private static bool CompareStraight(PokerHand firstInfo, PokerHand secondInfo)
        {
            if (!firstInfo.HasHighestStraight && secondInfo.HasHighestStraight)
            {
                return true;
            }

            if (firstInfo.HasHighestStraight && !secondInfo.HasHighestStraight)
            {
                return false;
            }

            return firstInfo.HighestCardValue < secondInfo.HighestCardValue;
        }

        private static bool CompareThreeOfAKind(PokerHand firstInfo, PokerHand secondInfo)
        {
            if (!firstInfo.HasThreeOfAKindAces && secondInfo.HasThreeOfAKindAces)
            {
                return true;
            }

            if (firstInfo.HasThreeOfAKindAces && !secondInfo.HasThreeOfAKindAces)
            {
                return false;
            }

            if (firstInfo.ThreeOfAKindKey == secondInfo.ThreeOfAKindKey)
            {
                var firstWithoutThreeIfAKindCards = new PokerHand(firstInfo.WithoutThreeOfAKindCards);
                var secondWithoutThreeIfAKindCards = new PokerHand(secondInfo.WithoutThreeOfAKindCards);

                return firstWithoutThreeIfAKindCards < secondWithoutThreeIfAKindCards;
            }

            return firstInfo.ThreeOfAKindKey < secondInfo.ThreeOfAKindKey;
        }

        private static int CompareOnePair(PokerHand firstInfo, PokerHand secondInfo)
        {
            if (!firstInfo.HasPairOfAces && secondInfo.HasPairOfAces)
            {
                return -1;
            }

            if (firstInfo.HasPairOfAces && !secondInfo.HasPairOfAces)
            {
                return 1;
            }

            if (firstInfo.Pairs.SequenceEqual(secondInfo.Pairs))
            {
                var withoutPair = new PokerHand(firstInfo.WithoutPairCards);
                var secondWithoutPair = new PokerHand(secondInfo.WithoutPairCards);

                return CompareTo(withoutPair, secondWithoutPair);
            }

            return firstInfo.Pairs.First().Value < secondInfo.Pairs.First().Value ? -1 : 1;
        }

        private static bool CompareTwoPairs(PokerHand firstInfo, PokerHand secondInfo)
        {
            if (firstInfo.HasPairOfAces && !secondInfo.HasPairOfAces)
            {
                return false;
            }

            if (!firstInfo.HasPairOfAces && secondInfo.HasPairOfAces)
            {
                return true;
            }

            if (firstInfo.HigherPairKey != secondInfo.HigherPairKey)
            {
                return firstInfo.HigherPairKey < secondInfo.HigherPairKey;
            }

            if (firstInfo.LowerPairKey != secondInfo.LowerPairKey)
            {
                return firstInfo.LowerPairKey < secondInfo.LowerPairKey;
            }

            return new PokerHand(firstInfo.WithoutPairCards) < new PokerHand(secondInfo.WithoutPairCards);
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