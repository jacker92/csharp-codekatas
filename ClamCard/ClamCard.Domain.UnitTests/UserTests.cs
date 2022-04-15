using ClamCard.Domain.Exceptions;
using ClamCard.Domain.Models;
using System;
using Xunit;

namespace ClamCard.Domain.UnitTests
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
            var exception = Assert.Throws<ArgumentException>(() => new User(string.Empty));
            Assert.Equal($"'name' cannot be null or whitespace. (Parameter 'name')", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldAssignNameCorrectly()
        {
            Assert.Equal(_username, _user.Name);
        }

        [Fact]
        public void ShouldNotHaveClamCardByDefault()
        {
            Assert.Null(_user.ClamCard);
        }

        [Fact]
        public void AddClamCard_ShouldThrowArgumentNullException_WithNullClamCard()
        {
            Assert.Throws<ArgumentNullException>(() => _user.AddClamCard(null));
        }

        [Fact]
        public void AddClamCard_ShouldCorreclyAssignClamCard()
        {
            var clamCard = new Models.ClamCard();
            _user.AddClamCard(clamCard);

            Assert.Equal(clamCard, _user.ClamCard);
        }

        [Fact]
        public void AddClamCard_ShouldClamCardAlreadyExistsException_IfUserAlreadyHasAClamCard()
        {
            var clamCard = new Models.ClamCard();
            _user.AddClamCard(clamCard);
            Assert.Throws<ClamCardAlreadyExistsException>(() => _user.AddClamCard(clamCard));
        }
    }
}