using System;
using Xunit;

namespace MemoryCache.Domain.Tests
{
    public class MemoryCacheOptionsTests
    {
        private readonly MemoryCacheOptions _memoryCacheOptions;
        public MemoryCacheOptionsTests()
        {
            _memoryCacheOptions = new MemoryCacheOptions();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Capacity_ShouldThrowArgumentOutOfRangeException_WithInvalidCapacity(int capacity)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _memoryCacheOptions.Capacity = capacity);
        }

        [Fact]
        public void ItemTimeToLive_ShouldThrowArgumentOutOfRangeException_WithTimeSpanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _memoryCacheOptions.ItemTimeToLive = TimeSpan.Zero);
        }

        [Fact]
        public void ItemTimeToLive_ShouldThrowArgumentOutOfRangeException_WithNegativeTimeSpan()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _memoryCacheOptions.ItemTimeToLive = TimeSpan.Zero.Subtract(TimeSpan.FromSeconds(1)));
        }
    }
}