using Xunit;

namespace TimesheetCalculator.Domain.Tests
{
    public class TimesheetCalculatorTests
    {
        [Fact]
        public void Calculate_ShouldWork()
        {
            var timesheetCalculator = new TimesheetCalculator();

            var result = timesheetCalculator.Calculate(new TimesheetTime(), new TimesheetTime());

            Assert.Equal("00:00", result.Duration.ToString());
        }
    }
}