using System;
using Xunit;

namespace MetricConverter.Domain.Tests
{
    public class MetricConverterTests
    {
        [Fact]
        public void ConvertKilometersToMiles_ShouldThrowArgumentOutOfRangeException_WithInvalidKilometerAmount()
        {
            var metricConverter = new MetricConverter();
            Assert.Throws<ArgumentOutOfRangeException>(() => metricConverter.ConvertKilometersToMiles(-1));
        }
    }
}