using System.Collections;
using System.Collections.Generic;

namespace PokerHands.Domain.Tests
{
    public class PokerHandCompareEqualityTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 5),
                },

                   new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Diamond, 1),
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Heart, 5),
                },
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                },

                   new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Diamond, 1),
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Heart, 4),
                    new PlayingCard(Suit.Heart, 5),
                },
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                },

                   new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Diamond, 2),
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Heart, 4),
                    new PlayingCard(Suit.Heart, 5),
                },
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 7),
                    new PlayingCard(Suit.Club, 5),
                },

                   new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 7),
                    new PlayingCard(Suit.Club, 5),
                },
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 7),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                },

                   new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Heart, 7),
                    new PlayingCard(Suit.Heart, 4),
                    new PlayingCard(Suit.Heart, 5),
                },
            };


            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 3),
                },

                   new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Diamond, 1),
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Heart, 3),
                },
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 3),
                },

                   new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Diamond, 1),
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Heart, 3),
                },
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                },

                   new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Heart, 4),
                    new PlayingCard(Suit.Heart, 5),
                },
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 13),
                    new PlayingCard(Suit.Club, 12),
                    new PlayingCard(Suit.Club, 11),
                    new PlayingCard(Suit.Club, 10),
                },

                   new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Heart, 13),
                    new PlayingCard(Suit.Heart, 12),
                    new PlayingCard(Suit.Heart, 11),
                    new PlayingCard(Suit.Heart, 10),
                },
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
