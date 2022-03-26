using System;
using Xunit;

namespace PokerHands.Domain.Tests
{
    public class PlayingCardTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(14)]
        public void ShouldThrowArgumentOutOfRangeException_IfCardValueIsOutOfRange(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PlayingCard(Suit.Club, value));
        }

        [Theory]
        [InlineData(Suit.Heart, 1, "AH")]
        [InlineData(Suit.Heart, 2, "2H")]
        [InlineData(Suit.Heart, 10, "TH")]
        [InlineData(Suit.Heart, 11, "JH")]
        [InlineData(Suit.Heart, 12, "QH")]
        [InlineData(Suit.Heart, 13, "KH")]
        [InlineData(Suit.Club, 13, "KC")]
        [InlineData(Suit.Diamond, 13, "KD")]
        [InlineData(Suit.Spade, 13, "KS")]
        public void ToString_ShouldReturnValidResult(Suit suit, int value, string expectedValue)
        {
            var card = new PlayingCard(suit, value);
            Assert.Equal(expectedValue, card.ToString());
        }

        [Fact]
        public void GetHashCode_ShouldReturnDifferentResultsForDifferentCards()
        {
            var card = new PlayingCard(Suit.Heart, 1);
            var card2 = new PlayingCard(Suit.Heart, 2);

            Assert.NotEqual(card.GetHashCode(), card2.GetHashCode());
        }
    }
}