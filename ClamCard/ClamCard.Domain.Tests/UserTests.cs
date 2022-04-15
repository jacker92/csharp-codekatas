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
        public void Constructor_ShouldSet0BalanceAsDefaultValue()
        {
            Assert.Equal(0, _user.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Deposit_ShouldThrowArgumentOutOfRangeException_IfAmountToDepositIsZeroOrNegative(double amount)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _user.Deposit(amount));
        }

        [Fact]
        public void Deposit_ShouldIncreaseBalance_BySetAmount()
        {
            _user.Deposit(10);

            Assert.Equal(10, _user.Balance);
        }
    }
}