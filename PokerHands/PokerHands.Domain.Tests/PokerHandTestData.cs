using System.Collections;
using System.Collections.Generic;

namespace PokerHands.Domain.Tests
{
    public class PokerHandTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2)
                },PokerHandRank.HighestCardTwo
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 3)
                },PokerHandRank.HighestCardThree
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                },PokerHandRank.HighestCardFour
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 5),
                },PokerHandRank.HighestCardFive
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 6),
                },PokerHandRank.HighestCardSix
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 7),
                },PokerHandRank.HighestCardSeven
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                },PokerHandRank.HighestCardEight
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 9),
                },PokerHandRank.HighestCardNine
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 10),
                },PokerHandRank.HighestCardTen
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 11),
                },PokerHandRank.HighestCardJack
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 12),
                },PokerHandRank.HighestCardQueen
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 13),
                },PokerHandRank.HighestCardKing
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 6),
                    new PlayingCard(Suit.Club, 1),
                },PokerHandRank.HighestCardAce
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 6),
                    new PlayingCard(Suit.Club, 1),
                },PokerHandRank.OnePair
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 1),
                },PokerHandRank.TwoPairs
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 1),
                },PokerHandRank.ThreeOfAKind
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                },PokerHandRank.Straight
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 13),
                    new PlayingCard(Suit.Club, 12),
                    new PlayingCard(Suit.Club, 11),
                    new PlayingCard(Suit.Club, 10),
                },PokerHandRank.Straight
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 12),
                    new PlayingCard(Suit.Club, 11),
                    new PlayingCard(Suit.Club, 10),
                },PokerHandRank.Flush
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Diamond, 1),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Diamond, 2),
                },PokerHandRank.FullHouse
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Diamond, 2),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Diamond, 5),
                },PokerHandRank.FullHouse
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Diamond, 2),
                    new PlayingCard(Suit.Spade, 2),
                    new PlayingCard(Suit.Diamond, 5),
                },PokerHandRank.FourOfAKind
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                },PokerHandRank.StraightFlush
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
