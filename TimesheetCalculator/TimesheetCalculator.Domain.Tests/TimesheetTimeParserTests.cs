using System;
using Xunit;

namespace TimesheetCalculator.Domain.Tests
{
    public class TimesheetTimeParserTests
    {
        [Fact]
        public void Parse_ShouldThrowArgumentException_WithEmptyTime()
        {
            var parser = new TimesheetTimeParser();
            Assert.Throws<ArgumentException>(() => parser.Parse(string.Empty));
        }

        [Theory]
        [InlineData("Hours are out of range.", "25:00")]
        [InlineData("Minutes are out of range.", "23:61")]
        [InlineData("Invalid format.", "0032")]
        [InlineData("Invalid format.", "00")]
        [InlineData("Invalid format.", "01a12")]
        [InlineData("Invalid format.", "01;12")]
        public void Parse_ShouldThrowTimesheetTimeParsingException_WithTimeInInvalidFormat_WithCorrectExceptionMessage(string expectedMessage, string time)
        {
            var parser = new TimesheetTimeParser();
            var exception = Assert.Throws<TimesheetTimeParsingException>(() => parser.Parse(time));
            Assert.Equal(expectedMessage, exception.Message);
        }

    }
}