﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHands.Domain
{
    public class PokerHandComparer : IComparer<PokerHand>
    {
        public int Compare(PokerHand? a, PokerHand? b)
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

                return Compare(new PokerHand(a.Cards.Take(a.Cards.Count() - 1)),
                       new PokerHand(b.Cards.Take(b.Cards.Count() - 1)));
            }

            return a.Rank > b.Rank ? 1 : -1;
        }

        private int CompareTwoHandsWithSameRank(PokerHand a, PokerHand b)
        {
            switch (a.Rank)
            {
                case PokerHandRank.FullHouse:
                    return CompareFullHouse(a, b);
                case PokerHandRank.Flush:
                    return CompareFlush(a, b);
                case PokerHandRank.Straight:
                    return CompareStraight(a, b);
                case PokerHandRank.ThreeOfAKind:
                    return CompareThreeOfAKind(a, b);
                case PokerHandRank.TwoPairs:
                    return CompareTwoPairs(a, b);
                case PokerHandRank.OnePair:
                    return CompareOnePair(a, b);

            }

            throw new Exception();
        }

        private int CompareFullHouse(PokerHand firstInfo, PokerHand secondInfo)
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

        private int CompareFlush(PokerHand firstInfo, PokerHand secondInfo)
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

            return Compare(new PokerHand(firstInfo.WithoutCurrentHighestCard), new PokerHand(secondInfo.WithoutCurrentHighestCard));
        }

        private int CompareStraight(PokerHand firstInfo, PokerHand secondInfo)
        {
            if (!firstInfo.HasHighestStraight && secondInfo.HasHighestStraight)
            {
                return -1;
            }

            if (firstInfo.HasHighestStraight && !secondInfo.HasHighestStraight)
            {
                return 1;
            }

            if (firstInfo.HighestCardValue != secondInfo.HighestCardValue)
            {
                return firstInfo.HighestCardValue < secondInfo.HighestCardValue ? -1 : 1;
            }

            return Compare(new PokerHand(firstInfo.WithoutCurrentHighestCard), new PokerHand(secondInfo.WithoutCurrentHighestCard));
        }

        private int CompareThreeOfAKind(PokerHand a, PokerHand b)
        {
            if (!a.HasThreeOfAKindAces && b.HasThreeOfAKindAces)
            {
                return -1;
            }

            if (a.HasThreeOfAKindAces && !b.HasThreeOfAKindAces)
            {
                return 1;
            }

            if (a.ThreeOfAKindKey != b.ThreeOfAKindKey)
            {
                return a.ThreeOfAKindKey < b.ThreeOfAKindKey ? -1 : 1;
            }

            var firstWithoutThreeIfAKindCards = new PokerHand(a.WithoutThreeOfAKindCards);
            var secondWithoutThreeIfAKindCards = new PokerHand(b.WithoutThreeOfAKindCards);

            return Compare(firstWithoutThreeIfAKindCards, secondWithoutThreeIfAKindCards);
        }

        private int CompareOnePair(PokerHand firstInfo, PokerHand secondInfo)
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

                return Compare(withoutPair, secondWithoutPair);
            }

            return firstInfo.Pairs.First().Value < secondInfo.Pairs.First().Value ? -1 : 1;
        }

        private int CompareTwoPairs(PokerHand firstInfo, PokerHand secondInfo)
        {
            if (!firstInfo.HasPairOfAces && secondInfo.HasPairOfAces)
            {
                return -1;
            }

            if (firstInfo.HasPairOfAces && !secondInfo.HasPairOfAces)
            {
                return 1;
            }

            if (firstInfo.HigherPairKey != secondInfo.HigherPairKey)
            {
                return firstInfo.HigherPairKey < secondInfo.HigherPairKey ? -1 : 1;
            }

            if (firstInfo.LowerPairKey != secondInfo.LowerPairKey)
            {
                return firstInfo.LowerPairKey < secondInfo.LowerPairKey ? -1 :1;
            }

            return Compare(new PokerHand(firstInfo.WithoutPairCards), new PokerHand(secondInfo.WithoutPairCards));
        }
    }
}
