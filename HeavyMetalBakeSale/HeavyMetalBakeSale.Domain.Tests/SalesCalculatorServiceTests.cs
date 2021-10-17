using HeavyMetalBakeSale.Domain.Models;
using HeavyMetalBakeSale.Domain.Models.DTO;
using HeavyMetalBakeSale.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace HeavyMetalBakeSale.Domain.Tests
{
    public class SalesCalculatorServiceTests
    {
        private readonly Mock<ISalesCalculationRequestParserService> _salesCalculationRequestParserService;
        private readonly Mock<ISalesCalculatorValidatorService> _salesCalculatorValidatorService;
        private readonly Mock<IItemCatalogService> _itemCatalogService;
        private readonly Mock<IPurchaseAssignmentConverterService> _itemRequestConverterService;
        private readonly Mock<IItemAmountCalculatorService> _itemAmountCalculatorService;
        private readonly SalesCalculatorService _salesCalculatorService;

        public SalesCalculatorServiceTests()
        {
            _salesCalculationRequestParserService = new Mock<ISalesCalculationRequestParserService>();
            _salesCalculatorValidatorService = new Mock<ISalesCalculatorValidatorService>();
            _itemCatalogService = new Mock<IItemCatalogService>();
            _itemRequestConverterService = new Mock<IPurchaseAssignmentConverterService>();
            _itemAmountCalculatorService = new Mock<IItemAmountCalculatorService>();
            _salesCalculatorService = new SalesCalculatorService(_salesCalculatorValidatorService.Object,
                                                                 _salesCalculationRequestParserService.Object,
                                                                 _itemCatalogService.Object,
                                                                 _itemRequestConverterService.Object,
                                                                 _itemAmountCalculatorService.Object);
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullSalesCalculatorValidatorService()
        {
            Assert.Throws<ArgumentNullException>(() => new SalesCalculatorService(null,
                                                                                  _salesCalculationRequestParserService.Object,
                                                                                  _itemCatalogService.Object,
                                                                                  _itemRequestConverterService.Object,
                                                                                  _itemAmountCalculatorService.Object));
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullSalesCalculationRequestParserService()
        {
            Assert.Throws<ArgumentNullException>(() => new SalesCalculatorService(_salesCalculatorValidatorService.Object,
                                                                                  null,
                                                                                  _itemCatalogService.Object,
                                                                                  _itemRequestConverterService.Object,
                                                                                  _itemAmountCalculatorService.Object));
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullItemCatalogService()
        {
            Assert.Throws<ArgumentNullException>(() => new SalesCalculatorService(_salesCalculatorValidatorService.Object,
                                                                                  _salesCalculationRequestParserService.Object,
                                                                                  null,
                                                                                  _itemRequestConverterService.Object,
                                                                                  _itemAmountCalculatorService.Object));
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullItemRequestConverterService()
        {
            Assert.Throws<ArgumentNullException>(() => new SalesCalculatorService(_salesCalculatorValidatorService.Object,
                                                                                  _salesCalculationRequestParserService.Object,
                                                                                  _itemCatalogService.Object,
                                                                                   null,
                                                                                  _itemAmountCalculatorService.Object));
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullItemAmountCalculatorService()
        {
            Assert.Throws<ArgumentNullException>(() => new SalesCalculatorService(_salesCalculatorValidatorService.Object,
                                                                                  _salesCalculationRequestParserService.Object,
                                                                                  _itemCatalogService.Object,
                                                                                   _itemRequestConverterService.Object,
                                                                                   null));
        }

        [Fact]
        public void CalculateTotal_ShouldThrowArgumentNullException_WithNullCalculationRequest()
        {
            Assert.Throws<ArgumentNullException>(() => _salesCalculatorService.CalculateTotal(null));
        }

        [Fact]
        public void CalculateTotal_ShouldReturnCorrectResult_IfItemsAreInStock()
        {
            var total = 5;
            var request = new SalesTotalCalculationRequest();
            var parsedList = new List<string> { "a" };
            var parsedCollection = new List<ParsedSalesTotalCalculationRequestResult>();
            var convertedItems = new List<PurchaseAssignment> {
                new PurchaseAssignment {
                CatalogItem = new CatalogStockItemDTO()
            }
            };

            _salesCalculationRequestParserService.Setup(x => x.Parse(request))
                .Returns(parsedCollection);

            _itemRequestConverterService.Setup(x => x.ConvertItems(parsedCollection))
                .Returns(convertedItems);

            _itemCatalogService.Setup(x => x.ItemsAreInStock(convertedItems))
                .Returns(true);

            _itemAmountCalculatorService.Setup(x => x.CalculateTotal(convertedItems))
                .Returns(total);

            var result = _salesCalculatorService.CalculateTotal(request);

            Assert.NotNull(result);
            Assert.Equal(SalesCalculationResultCode.AmountCalculated, result.CalculationResultCode);
            Assert.Equal(total, result.CalculatedAmount);
            Assert.Equal(convertedItems, result.PurchaseAssignment);

            _salesCalculatorValidatorService.Verify(x => x.ValidateSalesTotalCalculationRequest(request), Times.Once);
            _salesCalculationRequestParserService.Verify(x => x.Parse(request), Times.Once);
            _itemRequestConverterService.Verify(x => x.ConvertItems(parsedCollection), Times.Once);
            _itemCatalogService.Verify(x => x.ItemsAreInStock(convertedItems), Times.Once);
        }

        [Fact]
        public void CalculateChange_ShouldThrowArgumentNullException_WithNullRequest()
        {
            Assert.Throws<ArgumentNullException>(
                () => _salesCalculatorService.CalculateChange(null));
        }

        [Theory]
        [InlineData(10, 9, 1, SalesChangeCalculationResultCode.Change)]
        [InlineData(1, 1, 0, SalesChangeCalculationResultCode.NoChange)]
        [InlineData(1, 2, -1, SalesChangeCalculationResultCode.NotEnoughMoney)]
        public void CalculateChange_ShouldCalculateChangeCorrecly(double amountPaid, double totalAmount, double change, SalesChangeCalculationResultCode resultCode)
        {
            var changeCalculationRequest = new SalesChangeCalculationRequest
            {
                AmountPaid = amountPaid,
                TotalAmount = totalAmount
            };

            var result = _salesCalculatorService.CalculateChange(changeCalculationRequest);

            Assert.NotNull(result);
            Assert.Equal(change, result.Change);
            Assert.Equal(resultCode, result.CalculationResultCode);
        }
    }
}
