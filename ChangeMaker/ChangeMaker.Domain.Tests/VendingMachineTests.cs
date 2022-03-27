using System;
using System.Collections.Generic;
using Xunit;

namespace ChangeMaker.Domain.Tests
{
    public class VendingMachineTests
    {
        [Fact]
        public void ShouldThrowArgumentNullException_IfNullArrayOfCoinDenominationsIsGiven()
        {
            Assert.Throws<ArgumentNullException>(() => new VendingMachine(default(int[])));
        }

        [Fact]
        public void ShouldThrowArgumentNullException_IfNullCoinDenominationsIsGiven()
        {
            Assert.Throws<ArgumentNullException>(() => new VendingMachine(default(Denomination[])));
        }

        [Theory]
        [InlineData(1.25, 2.00, 25, 25, 25)]
        [InlineData(1.97, 2.00, 1, 1, 1)]
        public void CalculateChange_ShouldWork(double purchaseAmount, double tenderAmount, double change1, double change2, double change3)
        {
            var vendingMachine = new VendingMachine(CountryDemoninations.USDollar);
            var result = vendingMachine.CalculateChange(purchaseAmount, tenderAmount);

            Assert.Equal(3, result.Length);
            Assert.Equal(change1, result[0]);
            Assert.Equal(change2, result[1]);
            Assert.Equal(change3, result[2]);
        }

        [Theory]
        [InlineData(0, 1.01)]
        [InlineData(3, 1.04)]
        public void CalculateChange_WithFixedNumberOfDenominations_ShouldThrowDenominationNotFoundException_IfDenominationIsNotFound(int amountOfCoins, double tenderAmount)
        {
            var testDenominations = new List<Denomination>
            {
                new Denomination(1, amountOfCoins)
            };

            var vendingMachine = new VendingMachine(testDenominations);

            var exception = Assert.Throws<DenominationNotFoundException>(() =>
            vendingMachine.CalculateChange(1.00, tenderAmount));

            Assert.Equal("Denomination cannot be found.", exception.Message);
        }

        [Theory]
        [InlineData(1.25, 2.00, 25, 25, 25)]
        [InlineData(1.97, 2.00, 1, 1, 1)]
        public void CalculateChange_WithFixedNumberOfDenominations_ShouldWork(double purchaseAmount, double tenderAmount, double change1, double change2, double change3)
        {
            var testDenominations = new List<Denomination>
            {
                new Denomination(1, 3),
                new Denomination(5, 0),
                new Denomination(10, 0),
                new Denomination(25, 3)
            };

            var vendingMachine = new VendingMachine(testDenominations);

            var result = vendingMachine.CalculateChange(purchaseAmount, tenderAmount);

            Assert.Equal(3, result.Length);
            Assert.Equal(change1, result[0]);
            Assert.Equal(change2, result[1]);
            Assert.Equal(change3, result[2]);
        }
    }
}