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
    }
}