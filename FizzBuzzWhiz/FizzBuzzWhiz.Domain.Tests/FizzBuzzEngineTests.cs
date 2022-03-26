using Xunit;

namespace FizzBuzzWhiz.Domain.Tests
{
    public class FizzBuzzEngineTests
    {
        [Theory]
        [InlineData(1, "1")]
        [InlineData(2, "2")]
        [InlineData(3, "Fizz")]
        [InlineData(4, "4")]
        [InlineData(5, "Buzz")]
        [InlineData(6, "Fizz")]
        [InlineData(9, "Fizz")]
        [InlineData(10, "Buzz")]
        [InlineData(15, "FizzBuzz")]
        [InlineData(20, "Buzz")]
        [InlineData(30, "FizzBuzz")]
        [InlineData(75, "FizzBuzz")]
        public void Process_ShouldReturnCorrectResult(int input, string expected)
        {
            var engine = new FizzBuzzEngine();
            var result = engine.Process(input);
            Assert.Equal(expected, result);
        }
    }
}