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
        public void Process_ShouldReturnCorrectResult(int input, string expected)
        {
            var engine = new FizzBuzzEngine();
            var result = engine.Process(input);
            Assert.Equal(expected, result);
        }
    }
}