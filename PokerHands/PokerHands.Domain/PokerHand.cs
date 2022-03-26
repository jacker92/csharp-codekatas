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
            var info = new PokerHandInfo(this);

            if (info.HasFullHouse)
            {
                return PokerHandRank.FullHouse;
            }
            else if (info.HasFlush)
            {
                return PokerHandRank.Flush;
            }
            else if (info.HasStraight)
            {
                return PokerHandRank.Straight;
            }
            else if (info.HasThreeOfAKind)
            {
                return PokerHandRank.ThreeOfAKind;
            }
            else if (info.AmountOfPairs == 2)
            {
                return PokerHandRank.TwoPairs;
            }
            else if (info.AmountOfPairs == 1)
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
            var firstInfo = new PokerHandInfo(a);
            var secondInfo = new PokerHandInfo(b);

            switch (a.Rank)
            {
                case PokerHandRank.FullHouse:
                    return CompareFullHouse(firstInfo, secondInfo);
                case PokerHandRank.Flush:
                    return CompareFlush(firstInfo, secondInfo);
                case PokerHandRank.Straight:
                    return CompareStraight(firstInfo, secondInfo) ? -1 : 1;
                case PokerHandRank.ThreeOfAKind:
                    return CompareThreeOfAKind(firstInfo, secondInfo) ? -1 : 1;
                case PokerHandRank.TwoPairs:
                    return CompareTwoPairs(firstInfo, secondInfo) ? -1 : 1;
                case PokerHandRank.OnePair:
                    return CompareOnePair(firstInfo, secondInfo);

            }

            throw new Exception();
        }

        private static int CompareFullHouse(PokerHandInfo firstInfo, PokerHandInfo secondInfo)
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
                var firstPokerHandInfo = new PokerHandInfo(new PokerHand(firstInfo.WithoutThreeOfAKindCards));
                var secondPokerHandInfo = new PokerHandInfo(new PokerHand(secondInfo.WithoutThreeOfAKindCards));

                return CompareOnePair(firstPokerHandInfo, secondPokerHandInfo);
            }

            return firstInfo.ThreeOfAKindKey < secondInfo.ThreeOfAKindKey ? 1 : -1;
        }

        private static int CompareFlush(PokerHandInfo firstInfo, PokerHandInfo secondInfo)
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

        private static bool CompareStraight(PokerHandInfo firstInfo, PokerHandInfo secondInfo)
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

        private static bool CompareThreeOfAKind(PokerHandInfo firstInfo, PokerHandInfo secondInfo)
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

        private static int CompareOnePair(PokerHandInfo firstInfo, PokerHandInfo secondInfo)
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

        private static bool CompareTwoPairs(PokerHandInfo firstInfo, PokerHandInfo secondInfo)
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