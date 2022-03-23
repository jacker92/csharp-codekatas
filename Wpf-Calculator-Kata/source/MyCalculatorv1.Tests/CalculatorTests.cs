using System;
using Xunit;

namespace MyCalculatorv1.Tests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData("5+2", "=7")]
        [InlineData("5+10", "=15")]
        [InlineData("0+0", "=0")]
        [InlineData("-1+1", "=0")]
        [InlineData("1-2", "=-1")]
        [InlineData("5-2", "=3")]
        [InlineData("1/1", "=1")]
        [InlineData("2*9", "=18")]
        [InlineData("1/0", "=∞")]
        public void GetResult_ShouldReturnCorrectResult(string current, string expected)
        {
            var calc = new Calculator();
            var result = calc.GetResult(current);
            Assert.Equal(expected, result);
        }
    }
}