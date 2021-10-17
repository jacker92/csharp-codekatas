using HeavyMetalBakeSale.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HeavyMetalBakeSale.Domain.Tests
{
    public class CatalogItemDTOTests
    {
        private readonly CatalogItemDTO _catalogItemDTO;

        public CatalogItemDTOTests()
        {
            _catalogItemDTO = new CatalogItemDTO();
        }

        [Fact]
        public void ID_ShouldBeZeroByDefault()
        {
           Assert.Equal(0, _catalogItemDTO.ID);
        }

        [Fact]
        public void Name_ShouldBeNullByDefault()
        {
            Assert.Null(_catalogItemDTO.Name);
        }

        [Fact]
        public void Price_ShouldBeZeroByDefault()
        {
            Assert.Equal(0, _catalogItemDTO.Price);
        }
    }
}
