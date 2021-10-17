using AutoMapper;
using HeavyMetalBakeSale.Domain.Models;
using HeavyMetalBakeSale.Domain.Repositories;
using HeavyMetalBakeSale.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace HeavyMetalBakeSale.Domain.Tests
{
    public class PurchaseAssignmentConverterServiceTests
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ICatalogStockItemRepository> _catalogStockItemRepository;
        private readonly Mock<ICatalogItemAbbreviationMappingRepository> _catalogItemAbbreviationMappingRepository;
        private readonly PurchaseAssignmentConverterService _itemRequestConverterService;

        public PurchaseAssignmentConverterServiceTests()
        {
            _catalogStockItemRepository = new Mock<ICatalogStockItemRepository>();
            _mapper = new Mock<IMapper>();
            _catalogItemAbbreviationMappingRepository = new Mock<ICatalogItemAbbreviationMappingRepository>();
            _itemRequestConverterService = new PurchaseAssignmentConverterService(_catalogStockItemRepository.Object, _catalogItemAbbreviationMappingRepository.Object, _mapper.Object);
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullCatalogStockItemRepository()
        {
            Assert.Throws<ArgumentNullException>(() => new PurchaseAssignmentConverterService(null,
                                                                                              _catalogItemAbbreviationMappingRepository.Object,
                                                                                              _mapper.Object));
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullCatalogItemAbbreviationMappingRepository()
        {
            Assert.Throws<ArgumentNullException>(() => new PurchaseAssignmentConverterService(_catalogStockItemRepository.Object,
                                                                                              null,
                                                                                              _mapper.Object));
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullMapper()
        {
            Assert.Throws<ArgumentNullException>(() => new PurchaseAssignmentConverterService(_catalogStockItemRepository.Object,
                                                                                              _catalogItemAbbreviationMappingRepository.Object,
                                                                                              null));
        }

        [Fact]
        public void ConvertItems_ShouldThrowArgumentNullException_WithNullCollection()
        {
            Assert.Throws<ArgumentNullException>(() => _itemRequestConverterService.ConvertItems(null));
        }

        [Fact]
        public void ConvertItems_ShouldCallItemMappingRepository_ForMappings()
        {
            var item = "A";

            var calculationResult = new ParsedSalesTotalCalculationRequestResult { Abbreviation = item, Amount = 1 };

            _catalogItemAbbreviationMappingRepository.Setup(x => x.Get(item))
                .Returns(new CatalogItemAbbreviationMapping { Abbreviation = item });

            _catalogStockItemRepository.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new CatalogStockItem
                {
                    Item = new CatalogItem
                    {

                    }
                });

            var parsedCollection = new List<ParsedSalesTotalCalculationRequestResult>
            {
                calculationResult
            };
            var result = _itemRequestConverterService.ConvertItems(parsedCollection);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(calculationResult.Amount, result[0].AmountToPurchase);
        }
    }
}
