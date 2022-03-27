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
    }
}