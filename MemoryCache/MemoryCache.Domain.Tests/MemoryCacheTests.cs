using System;
using System.Collections.Generic;
using System.Linq;
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
        public void ShouldBeEmpty_AfterInitialization()
        {
            Assert.Empty(_memoryCache.Items);
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
            Assert.Throws<ArgumentException>(() => _memoryCache.SetItem(string.Empty));
        }
    }
}