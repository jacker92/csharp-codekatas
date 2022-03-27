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
    }
}