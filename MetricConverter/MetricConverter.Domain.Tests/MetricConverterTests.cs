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

        [Fact]
        public void CelsiusToFahrenheit_ShouldThrowArgumentOutOfRangeException_WithTemperatureLessThanAbsoluteZero()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _metricConverter.ConvertCelsiusToFahrenheit(-274));
            Assert.StartsWith("Argument cannot be less than absolute zero.", exception.Message);
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

        [Fact]
        public void KilogramToPound_ShouldThrowArgumentOutOfRangeException_IfKilogramIsLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _metricConverter.ConvertKilogramToPound(-1));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(5, 11.023113)]
        [InlineData(100, 220.462262)]
        public void KilogramToPound_ShouldReturnCorrectResult(double kilograms, double expectedPounds)
        {
            var result = _metricConverter.ConvertKilogramToPound(kilograms);
            Assert.Equal(expectedPounds, result, 6);
        }

        [Fact]
        public void LitersToGallons_ShouldThrowArgumentOutOfRangeException_IfLitersIsLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _metricConverter.ConvertLitersToGallons(-1, GallonTargetUnit.UK));
        }

        [Theory]
        [InlineData(0, GallonTargetUnit.UK, 0)]
        [InlineData(0, GallonTargetUnit.US, 0)]
        [InlineData(4.54609, GallonTargetUnit.UK, 1)]
        [InlineData(3.785411784, GallonTargetUnit.US, 1)]
        public void LitersToGallons_ShouldReturnCorrectResult(double liters, GallonTargetUnit targetUnit, double expectedGallons)
        {
            var result = _metricConverter.ConvertLitersToGallons(liters, targetUnit);
            Assert.Equal(expectedGallons, result,9);
        }
    }
}