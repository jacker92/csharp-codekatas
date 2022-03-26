using System;
using System.Collections.Generic;
using Xunit;

namespace PokerHands.Domain.Tests
{
    public class PokerHandTests
    {
        [Fact]
        public void ShouldThrowArgumentNullException_WithNullCardsArray()
        {
            Assert.Throws<ArgumentNullException>(() => new PokerHand(null));
        }

        [Theory]
        [ClassData(typeof(PokerHandTestData))]
        public void Rank_ShouldReturnCorrectResult(List<PlayingCard> cards, PokerHandRank expectedRank)
        {
            var hand = new PokerHand(cards);
            Assert.Equal(expectedRank, hand.Rank);
        }

        [Fact]
        public void Comparing_ShouldReturnCorrectResult_WithTwoHandsThatAreEqual()
        {
            var cards = new List<PlayingCard>
            {
                new PlayingCard(Suit.Diamond, 1),
                new PlayingCard(Suit.Diamond, 2),
                new PlayingCard(Suit.Diamond, 3),
                new PlayingCard(Suit.Diamond, 4),
                new PlayingCard(Suit.Diamond, 5)
            };

            var hand1 = new PokerHand(cards);
            var hand2 = new PokerHand(cards);

            Assert.True(hand1 == hand2);
            Assert.False(hand1 < hand2);
            Assert.False(hand2 < hand1);
            Assert.False(hand1 > hand2);
            Assert.False(hand2 > hand1);
        }

        [Theory]
        [ClassData(typeof(PokerHandCompareTestData))]
        public void Comparing_ShouldReturnCorrectResult(List<PlayingCard> cards1, List<PlayingCard> cards2)
        {
            var hand1 = new PokerHand(cards1);
            var hand2 = new PokerHand(cards2);

            Assert.True(hand1 < hand2);
        }

        [Theory]
        [ClassData(typeof(PokerHandCompareTestData))]
        public void Comparing_ShouldReturnCorrectResult_Inverted(List<PlayingCard> cards1, List<PlayingCard> cards2)
        {
            var hand1 = new PokerHand(cards1);
            var hand2 = new PokerHand(cards2);

            Assert.True(hand2 > hand1);
        }
    }
}