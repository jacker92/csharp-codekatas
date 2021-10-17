using AutoMapper;
using HeavyMetalBakeSale.Domain.Models;
using HeavyMetalBakeSale.Domain.Repositories;
using Moq;
using System;
using Xunit;

namespace HeavyMetalBakeSale.Domain.Tests
{
    public class ItemRepositoryTests
    {
        private readonly CatalogStockItemRepository _itemRepository;

        public ItemRepositoryTests()
        {
            _itemRepository = new CatalogStockItemRepository();
        }

        [Theory]
        [InlineData(1, 48)]
        [InlineData(2, 36)]
        [InlineData(3, 24)]
        [InlineData(4, 30)]
        public void Get_ShouldReturnCorrectResults_ForExistingItems(int id, int expectedAmount)
        {
            var item = _itemRepository.Get(id);

            Assert.Equal(expectedAmount, item.AmountInStock);
        }

        [Fact]
        public void GetCatalogItems_ShouldWork()
        {
            var result = _itemRepository.Get();

            Assert.NotNull(result);
        }

        [Fact]
        public void Update_ShouldThrowArgumentNullException_WithNullUpdateRequest()
        {
            Assert.Throws<ArgumentNullException>(() => _itemRepository.Update(null));
        }

        [Theory]
        [InlineData(2, 2)]
        [InlineData(0, 0)]
        [InlineData(-1, 0)]
        public void Update_ShouldUpdateItemCorrectly(int amountInStock, int expectedAmountInStock)
        {
            var stockItemID = 1;

            var request = new UpdateCatalogStockItemRequest
            {
                AmountInStock = amountInStock,
                CatalogStockItemID = stockItemID
            };

           var updatedItem = _itemRepository.Update(request);

            Assert.Equal(expectedAmountInStock, updatedItem.AmountInStock);
            Assert.Equal(stockItemID, updatedItem.ID);
        }

    }
}
