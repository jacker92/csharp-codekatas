using FluentAssertions;
using Xunit;

namespace BingoKata.Domain.Tests
{
    public class BingoCallerTests
    {
        [Fact]
        public void CallNumber_ShouldReturnNumber_Between1And75()
        {
            var bingoCaller = new BingoCaller();
            var result = bingoCaller.CallNumber();

            result.Should()
                .BeGreaterThanOrEqualTo(1)
                .And
                .BeLessThanOrEqualTo(75);
        }

        [Fact]
        public void CallNumber_ShouldThrowNoNumbersLeftException_After76Time()
        {
            var bingoCaller = new BingoCaller();
            for (int i = 0; i < 75; i++)
            {
                bingoCaller.CallNumber();
            }

            var exception = Assert.Throws<NoNumbersLeftException>(() => bingoCaller.CallNumber());

            Assert.Equal("No numbers left!", exception.Message);
        }
    }
}