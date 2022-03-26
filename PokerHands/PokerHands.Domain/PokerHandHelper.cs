namespace PokerHands.Domain
{
    internal static class PokerHandHelper
    {
        internal static IEnumerable<GroupedCard> GroupCards(IEnumerable<PlayingCard> cards)
        {
            return cards.GroupBy(x => x.Value)
               .Select(x => new GroupedCard { Key = x.Key, Count = x.Count(), Values = x.ToList() });
        }

        internal static bool HasFlush(IEnumerable<PlayingCard> cards)
        {
            if (cards.Count() != 5)
            {
                return false;
            }

            return cards.DistinctBy(x => x.Suit).Count() == 1;
        }

        internal static bool HasStraight(IEnumerable<PlayingCard> cards)
        {
            if (cards.Count() != 5)
            {
                return false;
            }

            var sequence = GetSequenceStartingFrom(cards.MinBy(x => x.Value)!.Value)
                .OrderBy(x => x);

            var cardValues = cards.Select(x => x.Value)
                .OrderBy(x => x);

            return cardValues.SequenceEqual(sequence) ||
                   cardValues.SequenceEqual(GetHighestStraightSequence());
        }

        internal static bool HasHighestStraightSequence(IEnumerable<PlayingCard> cards)
        {
            var cardValues = cards.Select(x => x.Value)
             .OrderBy(x => x);

            return cardValues.SequenceEqual(GetHighestStraightSequence());
        }

        internal static IEnumerable<int> GetHighestStraightSequence()
        {
            return new List<int> { 1, 10, 11, 12, 13 };
        }

        internal static IEnumerable<int> GetSequenceStartingFrom(int value)
        {
            return Enumerable.Range(value, 5);
        }

        internal class GroupedCard
        {
            public int Key { get; set; }
            public int Count { get; set; }
            public IEnumerable<PlayingCard>? Values { get; set; }
        }
    }
}
