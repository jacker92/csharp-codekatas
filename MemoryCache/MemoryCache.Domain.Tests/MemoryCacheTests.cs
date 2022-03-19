using System;
using System.Collections.Generic;
using Xunit;

namespace MemoryCache.Domain.Tests
{
    public class MemoryCacheTests
    {
        private readonly MemoryCache _memoryCache;
        public MemoryCacheTests()
        {
            _memoryCache = new MemoryCache();
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullMemoryCacheOptions()
        {
            Assert.Throws<ArgumentNullException>(() => new MemoryCache(null));
        }

        [Fact]
        public void SetTimeToLive_ShouldBeUsed()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions { ItemTimeToLive = TimeSpan.FromHours(1) });
            Assert.Equal(TimeSpan.FromHours(1), memoryCache.TimeToLive);
        }

        [Fact]
        public void Capacity_ShouldBe100000ByDefault()
        {
            Assert.Equal(100000, _memoryCache.Capacity);
        }

        [Fact]
        public void ShouldBeEmpty_AfterInitialization()
        {
            Assert.Equal(0, _memoryCache.CurrentCapacity);
        }

        [Fact]
        public void GetItem_ShouldThrowArgumentException_WithEmptyKey()
        {
            Assert.Throws<ArgumentException>(() => _memoryCache.GetItem(string.Empty));
        }

        [Fact]
        public void GetItem_ShouldThrowKeyNotFoundException_WithEmptyCache()
        {
            Assert.Throws<KeyNotFoundException>(() => _memoryCache.GetItem("asdf"));
        }

        [Fact]
        public void SetItem_ShouldThrowArgumentException_WithEmptyKey()
        {
            Assert.Throws<ArgumentException>(() => _memoryCache.SetItem(string.Empty, "test"));
        }

        [Fact]
        public void SetItem_ShouldThrowArgumentNullException_WithNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => _memoryCache.SetItem("test", null));
        }

        [Fact]
        public void SetItem_ShouldIncreaseCapacityByOne()
        {
            _memoryCache.SetItem("test", "test");
            Assert.Equal(1, _memoryCache.CurrentCapacity);
        }

        [Fact]
        public void SetItem_GetItem_ShouldReturnCorrectItem()
        {
            _memoryCache.SetItem("test", "test");
            var result = _memoryCache.GetItem("test");
            Assert.Equal("test", result.Item);
        }

        [Fact]
        public void SetItem_GetItem_ItemShouldHaveDefaultTTL()
        {
            _memoryCache.SetItem("test", "test");
            var result = _memoryCache.GetItem("test");
            var seconds = result.TimeToLive - DateTime.UtcNow;
            Assert.Equal(60, Math.Round(seconds.TotalSeconds));
        }

        [Fact]
        public void CapacityIsSetCorrectly()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions { Capacity = 1 });
            Assert.Equal(1, memoryCache.Capacity);
        }

        [Fact]
        public void CapacityExceeded_ShouldRemoveItemWithLeastTimeToLive()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions { Capacity = 1 });
            memoryCache.SetItem("test", "test");
            memoryCache.SetItem("test2", "test2");

            Assert.Equal(1, memoryCache.CurrentCapacity);
            var value = memoryCache.GetItem("test2");
            Assert.Equal("test2", value.Item);

            Assert.Throws<KeyNotFoundException>(() => memoryCache.GetItem("test"));
        }
    }
}