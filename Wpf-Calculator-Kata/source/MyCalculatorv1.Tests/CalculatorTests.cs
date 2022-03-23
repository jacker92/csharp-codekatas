using System;
using Xunit;

namespace MyCalculatorv1.Tests
{
    public class CalculatorTests
    {
        private readonly Calculator _calculator;

        public CalculatorTests()
        {
            _calculator = new Calculator();
        }

        [Theory]
        [InlineData("5+2", "5+2=7")]
        [InlineData("5+10", "5+10=15")]
        [InlineData("0+0", "0+0=0")]
        [InlineData("-1+1", "-1+1=0")]
        [InlineData("1-2", "1-2=-1")]
        [InlineData("5-2", "5-2=3")]
        [InlineData("1/1", "1/1=1")]
        [InlineData("2*9", "2*9=18")]
        [InlineData("1/0", "1/0=∞")]
        [InlineData("2*9=18", "2*9=18")]
        [InlineData("2*9=17", "2*9=17")]
        [InlineData("0/0", "Error!")]
        public void GetResult_ShouldReturnCorrectResult(string current, string expected)
        {
            var result = _calculator.GetResult(current);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("**")]
        public void GetResult_ShouldReturnErrorMessage_WhenExceptionIsThrown(string current)
        {
            var result =  _calculator.GetResult(current);
            Assert.Equal("Error!", result);
        }
    }
}