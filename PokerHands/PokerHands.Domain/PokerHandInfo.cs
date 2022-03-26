namespace PokerHands.Domain
{
    public class PokerHandInfo
    {
        private readonly PokerHand _pokerHand;

        public PokerHandInfo(PokerHand pokerHand)
        {
            _pokerHand = pokerHand;
        }

        internal bool HasFlush => PokerHandHelper.HasFlush(_pokerHand.Cards);
        internal bool HasAce => _pokerHand.Cards.Any(x => x.Value == 1);
        internal bool HasHighestStraight => PokerHandHelper.HasHighestStraightSequence(_pokerHand.Cards);
        internal int HighestCardValue => _pokerHand.Cards.MaxBy(x => x.Value)!.Value;
        internal IEnumerable<PokerHandHelper.GroupedCard> GroupedCards => PokerHandHelper.GroupCards(_pokerHand.Cards);
        internal bool HasThreeOfAKind => GroupedCards.Any(x => x.Count == 3);
        internal int AmountOfPairs => GroupedCards.Where(x => x.Count == 2).Count();
        internal bool HasStraight => PokerHandHelper.HasStraight(_pokerHand.Cards);
        internal bool HasThreeOfAKindAces => ThreeOfAKindKey == 1;
        internal int? ThreeOfAKindKey => GroupedCards.FirstOrDefault(x => x.Count == 3)?.Key;
        internal IEnumerable<PlayingCard> WithoutThreeOfAKindCards => GroupedCards.Where(x => x.Count != 3).SelectMany(x => x.Values);
        internal IEnumerable<PlayingCard> WithoutPairCards => GroupedCards.Where(x => x.Count != 2).SelectMany(x => x.Values);
        internal IEnumerable<PlayingCard> Pairs => GroupedCards.Where(x => x.Count == 2).SelectMany(x => x.Values);
        internal bool HasPairOfAces => Pairs.Any(x => x.Value == 1);
        internal int HigherPairKey => Pairs.MaxBy(x => x.Value)!.Value;
        internal int LowerPairKey => Pairs.MinBy(x => x.Value)!.Value;
        internal IEnumerable<PlayingCard> WithoutCurrentHighestCard => _pokerHand.Cards.OrderByDescending(x => x.Value).Take(_pokerHand.Cards.Count() - 1);
    }
}