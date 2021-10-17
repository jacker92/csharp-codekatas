using HeavyMetalBakeSale.Domain.Exceptions;
using HeavyMetalBakeSale.Domain.Repositories;
using System;
using Xunit;

namespace HeavyMetalBakeSale.Domain.Tests
{
    public class CatalogItemAbbreviationMappingRepositoryTests
    {
        private readonly CatalogItemAbbreviationMappingRepository _catalogItemAbbreviationMappingRepository;

        public CatalogItemAbbreviationMappingRepositoryTests()
        {
            _catalogItemAbbreviationMappingRepository = new CatalogItemAbbreviationMappingRepository();
        }

        [Fact]
        public void Get_ShouldThrowArgumentException_WithNullOrWhitespaceInput()
        {
            Assert.Throws<ArgumentException>(() => _catalogItemAbbreviationMappingRepository.Get(" "));
        }

        [Theory]
        [InlineData("B")]
        [InlineData("M")]
        [InlineData("C")]
        [InlineData("W")]
        public void Get_ShouldReturnCorrectResult_WithValidInput(string input)
        {
            var result = _catalogItemAbbreviationMappingRepository.Get(input);
            Assert.NotNull(result);
        }

        [Fact]
        public void Get_ShouldThrowCatalogItemMappingNotFoundException_WithMappingThatIsNotInDatabase()
        {
            Assert.Throws<CatalogItemMappingNotFoundException>(() =>
            _catalogItemAbbreviationMappingRepository.Get("ABC"));
        }
    }
}
