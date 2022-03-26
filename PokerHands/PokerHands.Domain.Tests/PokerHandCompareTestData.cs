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
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 7),
                },

                   new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                },
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 12),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 12),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 9),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 9),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 12),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 8),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 1),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 3),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 8),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 9),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 12),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 1),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 8),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 7),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 7),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 7),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 6),
                    new PlayingCard(Suit.Club, 6),
                    new PlayingCard(Suit.Club, 7),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 12),
                    new PlayingCard(Suit.Club, 12),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 8),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 12),
                    new PlayingCard(Suit.Club, 11),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 4),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 8),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 10),
                    new PlayingCard(Suit.Club, 12),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 10),
                    new PlayingCard(Suit.Club, 12),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 10),
                    new PlayingCard(Suit.Club, 11),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 10),
                    new PlayingCard(Suit.Club, 12),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 12),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                }
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
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 6),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 6),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 6),
                    new PlayingCard(Suit.Club, 7),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 6),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Club, 13),
                    new PlayingCard(Suit.Club, 12),
                    new PlayingCard(Suit.Club, 11),
                    new PlayingCard(Suit.Club, 10),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Heart, 4),
                    new PlayingCard(Suit.Heart, 5),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 12),
                    new PlayingCard(Suit.Club, 11),
                    new PlayingCard(Suit.Club, 10),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 8),
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Heart, 4),
                    new PlayingCard(Suit.Heart, 5),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 9),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 6),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                }
            };

            yield return new object[] {
                new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 8),
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Heart, 4),
                    new PlayingCard(Suit.Heart, 5),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 5),
                    new PlayingCard(Suit.Club, 6),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 4),
                }
            };

            yield return new object[] {
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 8),
                    new PlayingCard(Suit.Diamond, 2),
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Heart, 4),
                    new PlayingCard(Suit.Heart, 5),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 9),
                    new PlayingCard(Suit.Diamond, 9),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                }
            };

            yield return new object[] {
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Diamond, 2),
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Heart, 3),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Diamond, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 1),
                }
            };

            yield return new object[] {
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Diamond, 2),
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Heart, 3),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Diamond, 4),
                    new PlayingCard(Suit.Club, 4),
                }
            };

            yield return new object[] {
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Diamond, 1),
                    new PlayingCard(Suit.Club, 1),
                },      
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Diamond, 1),
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Heart, 3),
                }
            };

            yield return new object[] {
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 1),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Diamond, 2),
                    new PlayingCard(Suit.Club, 1),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Diamond, 2),
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Heart, 2),
                    new PlayingCard(Suit.Heart, 3),
                }
            };

            yield return new object[] {
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Diamond, 3),
                    new PlayingCard(Suit.Club, 1),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Diamond, 4),
                    new PlayingCard(Suit.Heart, 4),
                    new PlayingCard(Suit.Heart, 4),
                    new PlayingCard(Suit.Heart, 4),
                    new PlayingCard(Suit.Heart, 3),
                }
            };

            yield return new object[] {
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Club, 3),
                    new PlayingCard(Suit.Diamond, 3),
                    new PlayingCard(Suit.Club, 13),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Diamond, 3),
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Heart, 3),
                    new PlayingCard(Suit.Heart, 1),
                }
            };

            yield return new object[] {
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Club, 2),
                    new PlayingCard(Suit.Diamond, 2),
                    new PlayingCard(Suit.Club, 13),
                },
                 new List<PlayingCard> {
                    new PlayingCard(Suit.Diamond, 1),
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Heart, 1),
                    new PlayingCard(Suit.Heart, 3),
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
