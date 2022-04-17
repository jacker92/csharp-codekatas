using FluentAssertions;
using Xunit;

namespace BingoKata.Domain.Tests
{
    public class BingoCardGeneratorTests
    {
        [Fact]
        public void Generate_ShouldGenerateValidBingoCard()
        {
            var bingoNumberGenerator = new BingoNumberGenerator();
            var bingoCardGenerator = new BingoCardGenerator(bingoNumberGenerator);
            var bingoCard = bingoCardGenerator.Generate();

            ValidateBingoCard(bingoCard);
        }

        private static void ValidateBingoCard(BingoCard bingoCard)
        {
            bingoCard.SpaceRows.GetLength(0).Should().Be(5);
            bingoCard.SpaceRows.GetLength(1).Should().Be(5);

            for (int i = 0; i < 5; i++)
            {
                bingoCard.SpaceRows[0, i].Value.Should().BeGreaterThanOrEqualTo(Boundaries.Get(0)).And.BeLessThanOrEqualTo(Boundaries.Get(0) + 15);
                bingoCard.SpaceRows[1, i].Value.Should().BeGreaterThanOrEqualTo(Boundaries.Get(1)).And.BeLessThanOrEqualTo(Boundaries.Get(1) + 15);
                bingoCard.SpaceRows[3, i].Value.Should().BeGreaterThanOrEqualTo(Boundaries.Get(3)).And.BeLessThanOrEqualTo(Boundaries.Get(3) + 15);
                bingoCard.SpaceRows[4, i].Value.Should().BeGreaterThanOrEqualTo(Boundaries.Get(4)).And.BeLessThanOrEqualTo(Boundaries.Get(4) + 15);
            }

            bingoCard.SpaceRows[2, 0].Value.Should().BeGreaterThanOrEqualTo(Boundaries.Get(2)).And.BeLessThanOrEqualTo(Boundaries.Get(2) + 15);
            bingoCard.SpaceRows[2, 1].Value.Should().BeGreaterThanOrEqualTo(Boundaries.Get(2)).And.BeLessThanOrEqualTo(Boundaries.Get(2) + 15);
            bingoCard.SpaceRows[2, 2].Value.Should().BeNull();
            bingoCard.SpaceRows[2, 3].Value.Should().BeGreaterThanOrEqualTo(Boundaries.Get(2)).And.BeLessThanOrEqualTo(Boundaries.Get(2) + 15);
            bingoCard.SpaceRows[2, 4].Value.Should().BeGreaterThanOrEqualTo(Boundaries.Get(2)).And.BeLessThanOrEqualTo(Boundaries.Get(2) + 15);
        }
    }
}