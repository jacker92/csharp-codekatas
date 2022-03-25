using System;
using System.Collections.Generic;
using System.Linq;
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
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(6)]
        public void ShouldThrowArgumentException_WithTooManyCardsInCardsArray(int count)
        {
            var cards = Enumerable.Repeat(new PlayingCard(Suit.Diamond, 1), count).ToArray();
            var exception = Assert.Throws<ArgumentException>(() => new PokerHand(cards));
            Assert.Equal("Pokerhand must have five cards.", exception.Message);
        }

        [Theory]
        [ClassData(typeof(PokerHandTestData))]
        public void Rank_ShouldReturnCorrectResult(List<PlayingCard> cards, PokerHandRank expectedRank)
        {
            var hand = new PokerHand(cards);
            Assert.Equal(expectedRank, hand.Rank);
        }
    }
}