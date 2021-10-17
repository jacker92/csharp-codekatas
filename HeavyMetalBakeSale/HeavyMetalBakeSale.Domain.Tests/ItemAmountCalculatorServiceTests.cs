using HeavyMetalBakeSale.Domain.Models;
using HeavyMetalBakeSale.Domain.Models.DTO;
using HeavyMetalBakeSale.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HeavyMetalBakeSale.Domain.Tests
{
    public class ItemAmountCalculatorServiceTests
    {
        private readonly ItemAmountCalculatorService _itemAmountCalculatorService;

        public ItemAmountCalculatorServiceTests()
        {
            _itemAmountCalculatorService = new ItemAmountCalculatorService();
        }

        [Fact]
        public void CalculateTotal_ShouldThrowArgumentNullException_WithNullCollection()
        {
            Assert.Throws<ArgumentNullException>(() => _itemAmountCalculatorService.CalculateTotal(null));
        }

        [Fact]
        public void CalculateTotal_ShouldCalculateTotalOf0_ForEmptyCollection()
        {
            var result = _itemAmountCalculatorService.CalculateTotal(Enumerable.Empty<PurchaseAssignment>());
            Assert.Equal(0, result);
        }

        [Fact]
        public void CalculateTotal_ShouldCalculateTotalCorrectly()
        {
            var request = new List<PurchaseAssignment>
            {
                new PurchaseAssignment
                {
                    AmountToPurchase = 5,
                    CatalogItem = new CatalogStockItemDTO
                    {
                        Item = new CatalogItemDTO
                        {
                            Price = 10
                        }
                    }
                },
                 new PurchaseAssignment
                {
                    AmountToPurchase = 2,
                    CatalogItem = new CatalogStockItemDTO
                    {
                        Item = new CatalogItemDTO
                        {
                            Price = 1
                        }
                    }
                },
            };

            var result = _itemAmountCalculatorService.CalculateTotal(request);
            Assert.Equal(52, result);
        }
    }
}
