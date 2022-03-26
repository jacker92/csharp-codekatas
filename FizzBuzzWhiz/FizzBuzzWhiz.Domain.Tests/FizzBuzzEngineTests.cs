using Xunit;

namespace FizzBuzzWhiz.Domain.Tests
{
    public class FizzBuzzEngineTests
    {
        [Theory]
        [InlineData(1, 1)]
        public void Process_ShouldReturnCorrectResult(int input, int expected)
        {
            var engine = new FizzBuzzEngine();
            var result = engine.Process(input);
            Assert.Equal(expected, result);
        }
    }
}