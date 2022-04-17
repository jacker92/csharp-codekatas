using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BingoKata.Domain.Tests
{
    public class BingoCardCheckerTests
    {
        [Fact]
        public void HasBingo_ShouldBeFalse_IfNoBingoCanBeFound()
        {
            var bingoCardChecker = new BingoCardChecker();
            var bingoNumberGenerator = new BingoNumberGenerator();
            var bingoCardGenerator = new BingoCardGenerator(bingoNumberGenerator);
            var result = bingoCardChecker.HasBingo(new List<int> { 1}, bingoCardGenerator.Generate());

            result.Should().BeFalse();
        }

        [Fact]
        public void HasBingo_ShouldBeTrue_IfAllNumbersHaveBeenCalled()
        {
            var bingoCardChecker = new BingoCardChecker();
            var bingoNumberGenerator = new BingoNumberGenerator();
            var bingoCardGenerator = new BingoCardGenerator(bingoNumberGenerator);
            var result = bingoCardChecker.HasBingo(bingoNumberGenerator.GenerateNumbers().ToList(), bingoCardGenerator.Generate());

            result.Should().BeTrue();
        }
    }
}