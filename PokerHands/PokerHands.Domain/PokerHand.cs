using System.Linq;

namespace PokerHands.Domain
{
    public class PokerHand
    {
        private static readonly PokerHandComparer _pokerHandComparer = new();

        public PokerHand(IEnumerable<PlayingCard>? cards)
        {
            if (cards is null)
            {
                throw new ArgumentNullException(nameof(cards));
            }

            Cards = cards;
        }

        public IEnumerable<PlayingCard> Cards { get; }

        public PokerHandRank Rank => CalculateRank();
        
        internal bool HasStraightFlush => HasStraight && HasFlush;
        internal bool HasFullHouse => HasThreeOfAKind && AmountOfPairs == 1;
        internal bool HasFlush => PokerHandHelper.HasFlush(Cards);
        internal bool HasAce => Cards.Any(x => x.Value == 1);
        internal bool HasHighestStraight => PokerHandHelper.HasHighestStraightSequence(Cards);
        internal int HighestCardValue => Cards.MaxBy(x => x.Value)!.Value;
        internal IEnumerable<PokerHandHelper.GroupedCard> GroupedCards => PokerHandHelper.GroupCards(Cards);
        internal bool HasThreeOfAKind => GroupedCards.Any(x => x.Count == 3);
        internal bool HasFourOfAKind => GroupedCards.Any(x => x.Count == 4);
        internal int AmountOfPairs => GroupedCards.Where(x => x.Count == 2).Count();
        internal bool HasStraight => PokerHandHelper.HasStraight(Cards);
        internal bool HasThreeOfAKindAces => ThreeOfAKindKey == 1;
        internal bool HasFourOfAKindAces => FourOfAKindKey == 1;
        internal int ThreeOfAKindKey => GroupedCards.First(x => x.Count == 3).Key;
        internal int FourOfAKindKey => GroupedCards.First(x => x.Count == 4).Key;
        internal IEnumerable<PlayingCard> WithoutThreeOfAKindCards => GroupedCards.Where(x => x.Count != 3).SelectMany(x => x.Values!);
        internal IEnumerable<PlayingCard> WithoutFourOfAKindCards => GroupedCards.Where(x => x.Count != 4).SelectMany(x => x.Values!);
        internal IEnumerable<PlayingCard> WithoutPairCards => GroupedCards.Where(x => x.Count != 2).SelectMany(x => x.Values!);
        internal IEnumerable<PlayingCard> Pairs => GroupedCards.Where(x => x.Count == 2).SelectMany(x => x.Values!);
        internal bool HasPairOfAces => Pairs.Any(x => x.Value == 1);
        internal int HigherPairKey => Pairs.MaxBy(x => x.Value)!.Value;
        internal int LowerPairKey => Pairs.MinBy(x => x.Value)!.Value;
        internal IEnumerable<PlayingCard> WithoutCurrentHighestCard => Cards.OrderByDescending(x => x.Value).Take(Cards.Count() - 1);



        public static bool operator <(PokerHand a, PokerHand b)
        {
            var comparingResult = _pokerHandComparer.Compare(a, b);
            return comparingResult == -1;
        }

        public static bool operator >(PokerHand a, PokerHand b)
        {
            var comparingResult = _pokerHandComparer.Compare(a, b);
            return comparingResult == 1;
        }

        public static bool operator ==(PokerHand a, PokerHand b)
        {
            return _pokerHandComparer.Compare(a, b) == 0;
        }

        public static bool operator !=(PokerHand a, PokerHand b)
        {
            return !(a == b);
        }

        private PokerHandRank CalculateRank()
        {
            if (HasStraightFlush)
            {
                return PokerHandRank.StraightFlush;
            }
            else if (HasFourOfAKind)
            {
                return PokerHandRank.FourOfAKind;
            }
            else if (HasFullHouse)
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

            // When doing recursive comparison on hands, highest card can be smaller than 7.
            return max.Value switch
            {
                3 => PokerHandRank.HighestCardThree,
                4 => PokerHandRank.HighestCardFour,
                5 => PokerHandRank.HighestCardFive,
                6 => PokerHandRank.HighestCardSix,
                7 => PokerHandRank.HighestCardSeven,
                8 => PokerHandRank.HighestCardEight,
                9 => PokerHandRank.HighestCardNine,
                10 => PokerHandRank.HighestCardTen,
                11 => PokerHandRank.HighestCardJack,
                12 => PokerHandRank.HighestCardQueen,
                13 => PokerHandRank.HighestCardKing,
                _ => PokerHandRank.HighestCardTwo
            };
        }

        public override bool Equals(object? obj)
        {
            return obj is PokerHand hand &&
                   this == hand;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Cards, Rank);
        }
    }
}