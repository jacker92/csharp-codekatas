using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace LastSundayOfEachMonth.Domain.Tests
{
    public class DateProcessorTests
    {
        private readonly DateProcessor _dateProcessor;

        public DateProcessorTests()
        {
            _dateProcessor = new DateProcessor();
        }

        [Theory]
        [InlineData(1899)]
        [InlineData(10000)]
        public void LastSunday_ShouldThrowArgumentOutOfRangeException_WithYearOutOfRange(int year)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _dateProcessor.LastSunday(year));
        }

        [Theory]
        [MemberData(nameof(GetValues))]
        public void LastSunday_ShouldReturnCorrectResult_ForValue2013(string value)
        {
            var result = _dateProcessor.LastSunday(2013);

            Assert.NotNull(result);
            Assert.Equal(12, result.Count());

            Assert.Contains(DateTimeOffset.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal), result);
        }

        public static IEnumerable<object[]> GetValues()
        {
            yield return new object[] { "2013-01-27" };
            yield return new object[] { "2013-02-24" };
            yield return new object[] { "2013-03-31" };
            yield return new object[] { "2013-04-28" };
            yield return new object[] { "2013-05-26" };
            yield return new object[] { "2013-06-30" };
            yield return new object[] { "2013-07-28" };
            yield return new object[] { "2013-08-25" };
            yield return new object[] { "2013-09-29" };
            yield return new object[] { "2013-10-27" };
            yield return new object[] { "2013-11-24" };
            yield return new object[] { "2013-12-29" };
        }
    }
}
