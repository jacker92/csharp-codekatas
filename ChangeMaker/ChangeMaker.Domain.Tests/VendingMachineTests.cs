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

        [Theory]
        [InlineData(1.25, 2.00, 25, 25, 25)]
        [InlineData(1.97, 2.00, 1, 1, 1)]
        public void CalculateChange_ShouldWork(double purchaseAmount, double tenderAmount, double change1, double change2, double change3 )
        {
            var vendingMachine = new VendingMachine(new int[] { 1, 5, 10, 25 });
            var result = vendingMachine.CalculateChange(purchaseAmount, tenderAmount);

            Assert.Equal(3, result.Length);
            Assert.Equal(change1, result[0]);
            Assert.Equal(change2, result[1]);
            Assert.Equal(change3, result[2]);
        }
    }
}