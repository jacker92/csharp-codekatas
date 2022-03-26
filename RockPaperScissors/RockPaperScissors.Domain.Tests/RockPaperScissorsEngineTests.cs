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
        public void CalculateWinner_ShouldCalculateWinnerCorrectly(Gesture player1, Gesture player2, Gesture expectedGestureToWin)
        {
            var rockPaperScissorsEngine = new RockPaperScissorsEngine();

            var result = rockPaperScissorsEngine.CalculateWinner(player1, player2);

            Assert.Equal(expectedGestureToWin, result);
        }
    }
}