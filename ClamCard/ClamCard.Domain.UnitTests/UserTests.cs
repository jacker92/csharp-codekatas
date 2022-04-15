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
            var clamCard = new ClamCard();
            _user.AddClamCard(clamCard);

            Assert.Equal(clamCard, _user.ClamCard);
        }

        [Fact]
        public void AddClamCard_ShouldClamCardAlreadyExistsException_IfUserAlreadyHasAClamCard()
        {
            var clamCard = new ClamCard();
            _user.AddClamCard(clamCard);
            Assert.Throws<ClamCardAlreadyExistsException>(() => _user.AddClamCard(clamCard));
        }
    }
}