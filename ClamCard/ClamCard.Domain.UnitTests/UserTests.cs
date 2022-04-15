using System;
using Xunit;

namespace ClamCard.Domain.Tests
{
    public class ClamCardTests
    {

    }
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

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Withdraw_ShouldThrowArgumentOutOfRangeException_IfAmountToWithdrawIsZeroOrNegative(double amount)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _user.Withdraw(amount));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(500)]
        public void Withdraw_ShouldThrowInsufficientBalanceException_IfTriedToWithdraw_ButNotEnoughBalance(double amount)
        {
            var exception = Assert.Throws<InsufficientBalanceException>(() => _user.Withdraw(amount));
            Assert.Equal($"Cannot withdraw amount {amount} because user does not have enough balance.", exception.Message);
        }

        [Theory]
        [InlineData(10, 10, 0)]
        [InlineData(50, 10, 40)]
        public void Withdraw_ShouldDecreaseBalance_BySetAmount(double toDeposit, double ToWithdraw, double expected)
        {
            _user.Deposit(toDeposit);
            _user.Withdraw(ToWithdraw);

            Assert.Equal(expected, _user.Balance);
        }
    }
}