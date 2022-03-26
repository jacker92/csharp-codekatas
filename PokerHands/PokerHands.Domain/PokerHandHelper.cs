using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHands.Domain
{
    internal static class PokerHandHelper
    {
        internal static IEnumerable<GroupedCard> GroupCards(IEnumerable<PlayingCard> cards)
        {
            return cards.GroupBy(x => x.Value)
               .Select(x => new GroupedCard { Key = x.Key, Count = x.Count() });
        }

        internal static bool CardsAreInSequence(IEnumerable<PlayingCard> cards)
        {
            var sequence = GetSequenceStartingFrom(cards.MinBy(x => x.Value)!.Value);
            return cards.Select(x => x.Value).SequenceEqual(sequence);
        }

        internal static IEnumerable<int> GetSequenceStartingFrom(int value)
        {
            return Enumerable.Range(value, 5);
        }

        internal class GroupedCard
        {
            public int Key { get; set; }
            public int Count { get; set; }
        }
    }
}
