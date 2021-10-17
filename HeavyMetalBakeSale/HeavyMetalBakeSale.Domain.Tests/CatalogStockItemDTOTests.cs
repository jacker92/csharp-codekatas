using HeavyMetalBakeSale.Domain.Models.DTO;
using Xunit;

namespace HeavyMetalBakeSale.Domain.Tests
{
    public class CatalogStockItemDTOTests
    {
        private readonly CatalogStockItemDTO _catalogStockItemDTO;

        public CatalogStockItemDTOTests()
        {
            _catalogStockItemDTO = new CatalogStockItemDTO();
        }

        [Fact]
        public void ID_ShouldBeZeroByDefault()
        {
            Assert.Equal(0, _catalogStockItemDTO.ID);
        }

        [Fact]
        public void AmountInStock_ShouldBeZeroByDefault()
        {
            Assert.Equal(0, _catalogStockItemDTO.AmountInStock);
        }

        [Fact]
        public void Item_ShouldBeNullByDefault()
        {
            Assert.Null(_catalogStockItemDTO.Item);
        }
    }
}
