using System;
using Xunit;

namespace ClamCard.Domain.Tests
{
    public class UserTests
    {
        private readonly string _username;
        private readonly User _user;

        public UserTests()
        {
            _username = "test";
            _user = new User(_username);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentException_WithEmptyName()
        {
            Assert.Throws<ArgumentException>(() => new User(string.Empty));
        }

        [Fact]
        public void Constructor_ShouldAssignNameCorrectly()
        {
            Assert.Equal(_username, _user.Name);
        }

     
    }
}