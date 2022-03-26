using System;
using Xunit;

namespace MetricConverter.Domain.Tests
{
    public class MetricConverterTests
    {
        private readonly MetricConverter _metricConverter;

        public MetricConverterTests()
        {
            _metricConverter = new MetricConverter();
        }

        [Fact]
        public void ConvertKilometersToMiles_ShouldThrowArgumentOutOfRangeException_WithInvalidKilometerAmount()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _metricConverter.ConvertKilometersToMiles(-1));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0.621371)]
        [InlineData(75, 46.602825)]
        public void ConvertKilometersToMiles_ShouldReturnCorrectResult(double kilometers, double expectedMiles)
        {
            var result = _metricConverter.ConvertKilometersToMiles(kilometers);
            Assert.Equal(expectedMiles, result);
        }

        [Theory]
        [InlineData(0, 32)]
        [InlineData(30, 86)]
        [InlineData(100, 212)]
        public void CelsiusToFahrenheit_ShouldReturnCorrectResult(double celsius, double expectedFahrenheit)
        {
            var result = _metricConverter.ConvertCelsiusToFahrenheit(celsius);
            Assert.Equal(expectedFahrenheit, result);
        }
    }
}