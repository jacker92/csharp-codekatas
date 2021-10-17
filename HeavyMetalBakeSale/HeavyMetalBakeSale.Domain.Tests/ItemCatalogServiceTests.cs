using HeavyMetalBakeSale.Domain.Models;
using HeavyMetalBakeSale.Domain.Models.DTO;
using HeavyMetalBakeSale.Domain.Repositories;
using HeavyMetalBakeSale.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HeavyMetalBakeSale.Domain.Tests
{
    public class ItemCatalogServiceTests
    {
        private readonly Mock<ICatalogStockItemRepository> _catalogStockItemRepository;
        private readonly ItemCatalogService _itemCatalogService;

        public ItemCatalogServiceTests()
        {
            _catalogStockItemRepository = new Mock<ICatalogStockItemRepository>();
            _itemCatalogService = new ItemCatalogService(_catalogStockItemRepository.Object);
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullCatalogStockItemRepository()
        {
            Assert.Throws<ArgumentNullException>(() => new ItemCatalogService(null));
        }

        [Fact]
        public void ItemsAreInStock_ShouldThrowArgumentNullException_WithNullCollection()
        {
            Assert.Throws<ArgumentNullException>(() => _itemCatalogService.ItemsAreInStock(null));
        }

        [Fact]
        public void ItemsAreInStock_ShouldThrowArgumentException_WithEmptyCollection()
        {
            Assert.Throws<ArgumentException>(() => _itemCatalogService.ItemsAreInStock(Enumerable.Empty<PurchaseAssignment>()));
        }

        [Fact]
        public void ItemsAreInStock_ShouldReturnTrue_IfRequestedAmountDoesNotExceedAmountInStock()
        {
            _catalogStockItemRepository.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new CatalogStockItem
                {
                    AmountInStock = 1
                });

            var requests = new List<PurchaseAssignment>
            {
                new PurchaseAssignment
                {
                    CatalogItem = new CatalogStockItemDTO(),
                    AmountToPurchase = 1
                }
            };

            var result = _itemCatalogService.ItemsAreInStock(requests);

            Assert.True(result);
        }

        [Fact]
        public void ItemsAreInStock_ShouldReturnFalse_IfRequestedAmountExceedsAmountInStock()
        {
            var item = new CatalogStockItem
            {
                AmountInStock = 5
            };

            _catalogStockItemRepository.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(item);

            var requests = new List<PurchaseAssignment>
            {
                new PurchaseAssignment
                {
                    CatalogItem = new CatalogStockItemDTO(),
                    AmountToPurchase = 6
                }
            };

            var result = _itemCatalogService.ItemsAreInStock(requests);

            Assert.False(result);
        }

        [Fact]
        public void CommitPurchase_ShouldThrowArgumentNullException_WithNullCollection()
        {
            Assert.Throws<ArgumentNullException>(
                () => _itemCatalogService.CommitPurchase(null));
        }

        [Fact]
        public void CommitPurchase_ShouldNotCallItemRepository_WithEmptyCollection()
        {
            var list = new List<PurchaseAssignment>();

            _itemCatalogService.CommitPurchase(list);

            _catalogStockItemRepository.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(1, 5, 1, 4)]
        public void CommitPurchase_ShouldCallItemRepositoryCorrectly(int id, int amountInStock, int amountToPurchase, int amountExpectedAfterPurchase)
        {
            _catalogStockItemRepository.Setup(x => x.Get(id))
                .Returns(new CatalogStockItem
                {
                    ID = id,
                    AmountInStock = amountInStock
                });

            var list = new List<PurchaseAssignment>
            {
                new PurchaseAssignment
                {
                    AmountToPurchase = amountToPurchase,
                    CatalogItem = new CatalogStockItemDTO
                    {
                        ID = id
                    }
                }
            };

            _itemCatalogService.CommitPurchase(list);

            _catalogStockItemRepository.Verify(x => x.Update(It.Is<UpdateCatalogStockItemRequest>(
                y => y.AmountInStock == amountExpectedAfterPurchase)));
        }
    }
}
