using System;
using Xunit;

namespace PokerHands.Domain.Tests
{
    public class PlayingCardTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(15)]
        public void ShouldThrowArgumentOutOfRangeException_IfCardValueIsOutOfRange(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PlayingCard(Suit.Club, value));
        }

        [Theory]
        [InlineData(Suit.Heart, 1, "AH")]
        public void ToString_ShouldReturnValidResult(Suit suit, int value, string expectedValue)
        {
            var card = new PlayingCard(suit, value);
            Assert.Equal(expectedValue, card.ToString());
        }
    }
}