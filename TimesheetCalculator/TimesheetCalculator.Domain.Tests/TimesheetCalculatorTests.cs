using System;
using Xunit;

namespace TimesheetCalculator.Domain.Tests
{
    public class TimesheetCalculatorTests
    {
        private readonly TimesheetCalculator _timesheetCalculator;

        public TimesheetCalculatorTests()
        {
            _timesheetCalculator = new TimesheetCalculator();
        }

        [Theory]
        [InlineData(0, 0, 1, 0, "01:00")]
        [InlineData(0, 30, 1, 30, "01:00")]
        [InlineData(0, 30, 1, 00, "00:30")]
        [InlineData(23, 00, 23, 30, "00:30")]
        [InlineData(23, 30, 23, 59, "00:29")]
        [InlineData(23, 59, 23, 58, "00:01")]
        [InlineData(00, 00, 23, 59, "23:59")]
        [InlineData(23, 59, 00, 00, "23:59")]
        [InlineData(0, 0, 0, 0, "00:00")]
        public void Calculate_ShouldCalculateTimeCorrectly(int startTimeHours, int startTimeMinutes, int endTimeHours, int endTimeMinutes, string expectedResult)
        {
            var result = _timesheetCalculator.Calculate(new TimesheetTime(startTimeHours, startTimeMinutes), new TimesheetTime(endTimeHours, endTimeMinutes));

            Assert.Equal(expectedResult, result.Duration.ToString());
        }

        [Fact]
        public void Calculate_ShouldThrowArgumentNullException_IfStartTimeIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _timesheetCalculator.Calculate(null, new TimesheetTime()));
        }

        [Fact]
        public void Calculate_ShouldThrowArgumentNullException_IfEndTimeIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _timesheetCalculator.Calculate(new TimesheetTime(), null));
        }
    }
}