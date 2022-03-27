using Xunit;

namespace FluentCalculator.Domain.Tests
{
    public class CalculatorTests
    {
        private readonly Calculator _calculator;    
        public CalculatorTests()
        {
            _calculator = new Calculator();
        }

        [Fact]
        public void Seed_ShouldReturnCalculator()
        {
            var result = _calculator.Seed(5);
            Assert.Equal(_calculator, result);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        public void SeedAndResult_ShouldReturnSeedValue(int seed)
        {
            var result = _calculator.Seed(seed)
                                   .Result();

            Assert.Equal(result, seed);
        }

        [Theory]
        [InlineData(5, 10, 15)]
        [InlineData(10, 0, 10)]
        public void SeedAndPlus_ShouldReturnSeedValue(int seed, int toAdd, int expectedResult)
        {
            var result = _calculator
                .Seed(seed)
                .Plus(toAdd)
                .Result();

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(5, 10, 25)]
        [InlineData(10, 0, 10)]
        [InlineData(10, 5, 20)]
        [InlineData(10, -20, -30)]
        public void SeedAndPlusTwice_ShouldReturnSeedValue(int seed, int toAdd, int expectedResult)
        {
            var result = _calculator
                .Seed(seed)
                .Plus(toAdd)
                .Plus(toAdd)
                .Result();

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(5, 10, -5)]
        [InlineData(10, 0, 10)]
        [InlineData(10, 5, 5)]
        [InlineData(10, -20, 30)]
        public void SeedMinus_ShouldReturnCorrectResult(int seed, int toSubstract, int expectedResult)
        {
            var result = _calculator
             .Seed(seed)
             .Minus(toSubstract)
             .Result();

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(0)]
        public void SeedPlusAndUndo_ShouldReturnSeed(int seed)
        {
            var result = _calculator
                .Seed(seed)
                .Plus(seed)
                .Undo()
                .Result();

            Assert.Equal(seed, result);
        }

        [Theory]
        [InlineData(5, 1, 6)]
        [InlineData(0, 0, 0)]
        [InlineData(10, 50, 60)]
        public void SeedPlusAndUndoAndRedo_ShouldReturnAdditionResult(int seed, int toAdd, int expectedResult)
        {
            var result = _calculator
                .Seed(seed)
                .Plus(toAdd)
                .Undo()
                .Redo()
                .Result();

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        public void Undo_ShouldNotThrowException_EvenIfNothingToUndo(int seed)
        {
            var result = _calculator
                .Seed(seed)
                .Undo()
                .Undo()
                .Result();

            Assert.Equal(seed, result);
        }


        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        public void Redo_ShouldNotThrowException_EvenIfNothingToRedo(int seed)
        {
            var result = _calculator
                .Seed(seed)
                .Redo()
                .Redo()
                .Result();

            Assert.Equal(seed, result);
        }
    }
}