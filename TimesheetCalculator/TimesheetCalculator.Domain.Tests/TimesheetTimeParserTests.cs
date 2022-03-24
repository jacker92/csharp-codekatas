using System;
using Xunit;

namespace TimesheetCalculator.Domain.Tests
{
    public class TimesheetTimeParserTests
    {
        private readonly TimesheetTimeParser _timesheetTimeParser;

        public TimesheetTimeParserTests()
        {
            _timesheetTimeParser = new TimesheetTimeParser();
        }

        [Fact]
        public void Parse_ShouldThrowArgumentException_WithEmptyTime()
        {
            Assert.Throws<ArgumentException>(() => _timesheetTimeParser.Parse(string.Empty));
        }

        [Theory]
        [InlineData("Hours are out of range.", "25:00")]
        [InlineData("Hours are out of range.", "13:12 PM")]
        [InlineData("Minutes are out of range.", "23:61")]
        [InlineData("Minutes are out of range.", "999")]
        [InlineData("Invalid format.", "00")]
        [InlineData("Invalid format.", "01a12")]
        [InlineData("Invalid format.", "01;12")]
        [InlineData("Invalid format.", "l22")]
        [InlineData("Invalid format.", "12p")]
        [InlineData("Invalid format.", " 1 ")]
        public void Parse_ShouldThrowTimesheetTimeParsingException_WithTimeInInvalidFormat_WithCorrectExceptionMessage(string expectedMessage, string time)
        {
            var exception = Assert.Throws<TimesheetTimeParsingException>(() => _timesheetTimeParser.Parse(time));
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData("00:00", 0,0)]
        [InlineData("02:00", 2,0)]
        [InlineData("23:59", 23,59)]
        [InlineData("259", 2,59)]
        [InlineData("2059", 20,59)]
        [InlineData(" 2059 ", 20,59)]
        [InlineData("08:00 AM", 08,00)]
        [InlineData("08:00 PM", 20,00)]
        [InlineData("12:00 PM", 12,0)]
        [InlineData("12:00 AM", 0,0)]
        [InlineData("12:59 AM", 0,59)]
        [InlineData("1:00 AM", 1,0)]
        [InlineData("11:59 PM", 23,59)]
        public void Parse_ShouldReturnTimeInCorrectFormat(string time, int hours, int minutes)
        {
            var result = _timesheetTimeParser.Parse(time);

            Assert.Equal(hours, result.Hours);
            Assert.Equal(minutes, result.Minutes);
        }
    }
}