using FluentAssertions;
using Xunit;

namespace BingoKata.Domain.Tests
{
    public class BingoCallerTests
    {
        private readonly BingoCaller _bingoCaller;

        public BingoCallerTests()
        {
            _bingoCaller = new BingoCaller();
        }

        [Fact]
        public void CallNumber_ShouldReturnNumber_Between1And75()
        {
            var result = _bingoCaller.CallNumber();

            result.Should()
                .BeGreaterThanOrEqualTo(1)
                .And
                .BeLessThanOrEqualTo(75);
        }

        [Fact]
        public void CallNumber_ShouldThrowNoNumbersLeftException_After76Time()
        {
            for (int i = 0; i < 75; i++)
            {
                _bingoCaller.CallNumber();
            }

            var exception = Assert.Throws<NoNumbersLeftException>(() => _bingoCaller.CallNumber());
            exception.Message.Should().Be("No numbers left!");
        }
    }
}