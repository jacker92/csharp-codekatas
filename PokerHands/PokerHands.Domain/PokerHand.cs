﻿using System.Linq;

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
            if (a == b)
            {
                return false;
            }
            return !(a > b);
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
            var info = new PokerHandInfo(this);

            if (info.CardsAreInSequence)
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

        private static bool CompareTwoHandsWithSameRank(PokerHand a, PokerHand b)
        {
            var firstInfo = new PokerHandInfo(a);
            var secondInfo = new PokerHandInfo(b);

            switch (a.Rank)
            {
                case PokerHandRank.ThreeOfAKind:
                    return CompareThreeOfAKind(firstInfo, secondInfo);
                case PokerHandRank.TwoPairs:
                    return CompareTwoPairs(firstInfo, secondInfo);
                case PokerHandRank.OnePair:
                    return CompareOnePair(firstInfo, secondInfo);

            }

            throw new Exception();
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

        private static bool CompareOnePair(PokerHandInfo firstInfo, PokerHandInfo secondInfo)
        {
            if (!firstInfo.HasPairOfAces && secondInfo.HasPairOfAces)
            {
                return true;
            }

            if (firstInfo.HasPairOfAces && !secondInfo.HasPairOfAces)
            {
                return false;
            }

            if (firstInfo.Pairs.SequenceEqual(secondInfo.Pairs))
            {
                var withoutPair = new PokerHand(firstInfo.WithoutPairCards);
                var secondWithoutPair = new PokerHand(secondInfo.WithoutPairCards);

                return withoutPair < secondWithoutPair;
            }

            return firstInfo.Pairs.First().Value < secondInfo.Pairs.First().Value;
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

            // TODO: Need to check if high card is ace, might return invalid result here
            var highCardResult = firstInfo.WithoutPairCards.Single().Value < secondInfo.WithoutPairCards.Single().Value;

            return highCardResult;
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