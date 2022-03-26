using Xunit;

namespace RockPaperScissors.Domain.Tests
{
    public class RockPaperScissorsEngineTests
    {
        [Theory]
        [InlineData(Gesture.Rock, Gesture.Rock, Gesture.Rock)]
        [InlineData(Gesture.Rock, Gesture.Scissors, Gesture.Rock)]
        [InlineData(Gesture.Scissors, Gesture.Rock, Gesture.Rock)]
        [InlineData(Gesture.Scissors, Gesture.Paper, Gesture.Scissors)]
        [InlineData(Gesture.Scissors, Gesture.Scissors, Gesture.Scissors)]
        [InlineData(Gesture.Paper, Gesture.Scissors, Gesture.Scissors)]
        [InlineData(Gesture.Paper, Gesture.Paper, Gesture.Paper)]
        [InlineData(Gesture.Paper, Gesture.Rock, Gesture.Paper)]
        [InlineData(Gesture.Rock, Gesture.Paper, Gesture.Paper)]
        public void CalculateWinner_ShouldCalculateWinnerCorrectly(Gesture player1, Gesture player2, Gesture expectedGestureToWin)
        {
            var rockPaperScissorsEngine = new RockPaperScissorsEngine();

            var result = rockPaperScissorsEngine.CalculateWinner(player1, player2);

            Assert.Equal(expectedGestureToWin, result);
        }
    }
}