using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BingoKata.Domain.Tests
{
    public class BingoCardCheckerTests
    {
        private readonly BingoCardChecker _bingoCardChecker;
        private readonly BingoNumberGenerator _bingoNumberGenerator;
        private readonly BingoCardGenerator _bingoCardGenerator;

        public BingoCardCheckerTests()
        {
            _bingoCardChecker = new BingoCardChecker();
            _bingoNumberGenerator = new BingoNumberGenerator();
            _bingoCardGenerator = new BingoCardGenerator(_bingoNumberGenerator);
        }

        [Fact]
        public void HasBingo_ShouldBeFalse_IfNoBingoCanBeFound()
        {
            var bingoCard = _bingoCardGenerator.Generate();
            var result = _bingoCardChecker.HasBingo(new List<int> { 1 }, bingoCard);

            result.Should().BeFalse();
        }

        [Fact]
        public void HasBingo_ShouldBeTrue_IfAllNumbersHaveBeenCalled()
        {
            var bingoCard = _bingoCardGenerator.Generate();
            var numbers = _bingoNumberGenerator.GenerateNumbers().ToList();

            var result = _bingoCardChecker.HasBingo(numbers, bingoCard);

            result.Should().BeTrue();
        }

        [Fact]
        public void HasBingo_ShouldBeTrue_IfHasVerticalBingo()
        {
            var bingoCard = _bingoCardGenerator.Generate();
            var numbers = new List<int>();

            for (int y = 0; y < 5; y++)
            {
                numbers.Add(bingoCard.SpaceRows[0, y].Value!.Value);
            }

            var result = _bingoCardChecker.HasBingo(numbers, bingoCard);

            result.Should().BeTrue();
        }

        [Fact]
        public void HasBingo_ShouldBeTrue_IfHasHorizontalBingo()
        {
            var bingoCard = _bingoCardGenerator.Generate();
            var numbers = new List<int>();

            for (int x = 0; x < 5; x++)
            {
                numbers.Add(bingoCard.SpaceRows[x, 0].Value!.Value);
            }

            var result = _bingoCardChecker.HasBingo(numbers, bingoCard);

            result.Should().BeTrue();
        }

        [Fact]
        public void HasBingo_ShouldBeTrue_IfHasHorizontalInCenterBingo()
        {
            var bingoCard = _bingoCardGenerator.Generate();
            var numbers = new List<int>();

            for (int x = 0; x < 5; x++)
            {
                numbers.Add(bingoCard.SpaceRows[x, 2].Value ?? -1);
            }

            var result = _bingoCardChecker.HasBingo(numbers, bingoCard);

            result.Should().BeTrue();
        }

        [Fact]
        public void HasBingo_ShouldBeTrue_IfHasDiagonalBingo_FromUpperLeftCorner_ToLowerRightCorner()
        {
            var bingoCard = _bingoCardGenerator.Generate();
            var numbers = new List<int>();

            for (int x = 0; x < 5; x++)
            {
                numbers.Add(bingoCard.SpaceRows[x, x].Value ?? -1);
            }

            var result = _bingoCardChecker.HasBingo(numbers, bingoCard);

            result.Should().BeTrue();
        }

        [Fact]
        public void HasBingo_ShouldBeTrue_IfHasDiagonalBingo_FromLowerLeftCorner_ToUpperRightCorner()
        {
            var bingoCard = _bingoCardGenerator.Generate();
            var numbers = new List<int>();

            for (int x = 4; x >= 0; x--)
            {
                numbers.Add(bingoCard.SpaceRows[x, 4 - x].Value ?? -1);
            }

            var result = _bingoCardChecker.HasBingo(numbers, bingoCard);

            result.Should().BeTrue();
        }
    }
}