using ClamCard.Domain.Exceptions;
using System;
using Xunit;

namespace ClamCard.Domain.UnitTests
{
    public class ClamCardTests
    {
        private readonly Models.ClamCard _clamCard;

        public ClamCardTests()
        {
            _clamCard = new Models.ClamCard();
        }

        [Fact]
        public void Constructor_ShouldSet0BalanceAsDefaultValue()
        {
            Assert.Equal(0, _clamCard.Balance);
        }

        [Fact]
        public void Constructor_ShouldSet100Balance_IfExplicitlySet()
        {
            var clamCard = new Models.ClamCard(100);
            Assert.Equal(100, clamCard.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Deposit_ShouldThrowArgumentOutOfRangeException_IfAmountToDepositIsZeroOrNegative(double amount)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _clamCard.Deposit(amount));
        }

        [Fact]
        public void Deposit_ShouldIncreaseBalance_BySetAmount()
        {
            _clamCard.Deposit(10);

            Assert.Equal(10, _clamCard.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Withdraw_ShouldThrowArgumentOutOfRangeException_IfAmountToWithdrawIsZeroOrNegative(double amount)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _clamCard.Withdraw(amount));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(500)]
        public void Withdraw_ShouldThrowInsufficientBalanceException_IfTriedToWithdraw_ButNotEnoughBalance(double amount)
        {
            var exception = Assert.Throws<InsufficientBalanceException>(() => _clamCard.Withdraw(amount));
            Assert.Equal($"Cannot withdraw amount {amount} because user does not have enough balance.", exception.Message);
        }

        [Theory]
        [InlineData(10, 10, 0)]
        [InlineData(50, 10, 40)]
        public void Withdraw_ShouldDecreaseBalance_BySetAmount(double toDeposit, double ToWithdraw, double expected)
        {
            _clamCard.Deposit(toDeposit);
            _clamCard.Withdraw(ToWithdraw);

            Assert.Equal(expected, _clamCard.Balance);
        }
    }
}