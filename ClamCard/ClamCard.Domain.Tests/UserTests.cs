using System;
using Xunit;

namespace ClamCard.Domain.Tests
{
    public class UserTests
    {
        [Fact]
        public void Constructor_ShouldThrowArgumentException_WithEmptyName()
        {
            Assert.Throws<ArgumentException>(() => new User(string.Empty));
        }
    }
}