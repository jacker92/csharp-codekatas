using System.Collections;
using System.Collections.Generic;

namespace PokerHands.Domain.Tests
{
    public class PokerHandCompareTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 7),
                },

                   new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                },
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 12),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 12),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 9),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 9),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 12),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 8),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 8),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 9),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 8),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 7),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 7),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 7),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 6),
                    new PlayingCard(Suit.Club, 6),
                    new PlayingCard(Suit.Club, 7),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 12),
                    new PlayingCard(Suit.Club, 12),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 8),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 12),
                    new PlayingCard(Suit.Club, 11),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 10),
                    new PlayingCard(Suit.Club, 12),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 10),
                    new PlayingCard(Suit.Club, 12),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 10),
                    new PlayingCard(Suit.Club, 11),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 10),
                    new PlayingCard(Suit.Club, 12),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 12),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
