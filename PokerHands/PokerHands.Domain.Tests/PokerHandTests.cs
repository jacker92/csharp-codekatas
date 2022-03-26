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

        [Theory]
        [ClassData(typeof(PokerHandCompareEqualityTestData))]
        public void Comparing_ShouldReturnCorrectResult_WithTwoHandsThatAreEqual(List<PlayingCard> cards1, List<PlayingCard> cards2)
        {
            var hand1 = new PokerHand(cards1);
            var hand2 = new PokerHand(cards2);

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