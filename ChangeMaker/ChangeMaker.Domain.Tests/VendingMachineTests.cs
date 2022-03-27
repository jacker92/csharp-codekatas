using System;
using Xunit;

namespace ChangeMaker.Domain.Tests
{
    public class VendingMachineTests
    {
        /*
         * var coinDenominations = [1,5,10,25]; // coin values converted to whole numbers
            var machine = new VendingMachine(coinDenominations);
            var purchaseAmount = 1.25; // amount the item cost
            var tenderAmount = 2.00; // amount the user input for the purchase
            var change = machine.CalculateChange(purchaseAmount, tenderAmount);
         * 
         */

        [Fact]
        public void ShouldThrowArgumentNullException_IfNullCoinDenominationsIsGiven()
        {
            Assert.Throws<ArgumentNullException>(() => new VendingMachine(null));
        }

        [Fact]
        public void CalculateChange_ShouldWork()
        {
            var vendingMachine = new VendingMachine(new int[] { 1, 5, 10, 25 });
            var result = vendingMachine.CalculateChange(1.25, 2.00);

            Assert.Equal(3, result.Length);
            Assert.Equal(25, result[0]);
            Assert.Equal(25, result[1]);
            Assert.Equal(25, result[2]);
        }
    }
}