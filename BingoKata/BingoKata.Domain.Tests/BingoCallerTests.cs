using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BingoKata.Domain.Tests
{
    public class BingoCallerTests
    {
        private readonly BingoCaller _bingoCaller;
        private readonly BingoNumberGenerator _bingoNumberGenerator;

        public BingoCallerTests()
        {
            _bingoNumberGenerator = new BingoNumberGenerator();
            _bingoCaller = new BingoCaller(_bingoNumberGenerator);
        }

        [Fact]
        public void CallNumber_ShouldReturnNumber_Between1And75()
        {
            var result = _bingoCaller.CallNumber();

            ShouldBeBetween1And75(result);
        }

        [Fact]
        public void CallNumber_ShouldThrowNoNumbersLeftException_After76Time()
        {
            InvokeCallNumber75Times();

            var exception = Assert.Throws<NoNumbersLeftException>(() => _bingoCaller.CallNumber());
            exception.Message.Should().Be("No numbers left!");
        }

        [Fact]
        public void CallNumber_Called75Times_AllNumbersShouldHaveBeenCalled_WithNoDuplicates()
        {
            var numbers = InvokeCallNumber75Times();

            numbers.Should().OnlyHaveUniqueItems();
            numbers.Should().AllSatisfy(x => ShouldBeBetween1And75(x));
        }

        private List<int> InvokeCallNumber75Times()
        {
            var numbers = new List<int>();
            for (int i = 0; i < 75; i++)
            {
                var number = _bingoCaller.CallNumber();
                numbers.Add(number);
            }

            return numbers;
        }

        private static void ShouldBeBetween1And75(int result)
        {
            result.Should()
                .BeGreaterThanOrEqualTo(1)
                .And
                .BeLessThanOrEqualTo(75);
        }
    }
}