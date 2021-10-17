using HeavyMetalBakeSale.Domain.Models;
using System;

namespace HeavyMetalBakeSale.Domain.Services
{
    public class SalesCalculatorService : ISalesCalculatorService
    {
        private readonly ISalesCalculatorValidatorService _salesCalculatorValidatorService;
        private readonly ISalesCalculationRequestParserService _salesCalculationRequestParserService;
        private readonly IItemCatalogService _itemCatalogService;
        private readonly IPurchaseAssignmentConverterService _itemRequestConverterService;
        private readonly IItemAmountCalculatorService _itemAmountCalculatorService;

        public SalesCalculatorService(ISalesCalculatorValidatorService salesCalculatorValidatorService,
                                      ISalesCalculationRequestParserService salesCalculationRequestParserService,
                                      IItemCatalogService itemCatalogService,
                                      IPurchaseAssignmentConverterService itemRequestConverterService,
                                      IItemAmountCalculatorService itemAmountCalculatorService)
        {
            _salesCalculatorValidatorService = salesCalculatorValidatorService ?? throw new ArgumentNullException(nameof(salesCalculatorValidatorService));
            _salesCalculationRequestParserService = salesCalculationRequestParserService ?? throw new ArgumentNullException(nameof(salesCalculationRequestParserService));
            _itemCatalogService = itemCatalogService ?? throw new ArgumentNullException(nameof(itemCatalogService));
            _itemRequestConverterService = itemRequestConverterService ?? throw new ArgumentNullException(nameof(itemRequestConverterService));
            _itemAmountCalculatorService = itemAmountCalculatorService ?? throw new ArgumentNullException(nameof(itemAmountCalculatorService));
        }

        public SalesTotalCalculationResult CalculateTotal(SalesTotalCalculationRequest calculationRequest)
        {
            if (calculationRequest is null)
            {
                throw new ArgumentNullException(nameof(calculationRequest));
            }

            _salesCalculatorValidatorService.ValidateSalesTotalCalculationRequest(calculationRequest);

            var parsedItems = _salesCalculationRequestParserService.Parse(calculationRequest);

            var convertedItems = _itemRequestConverterService.ConvertItems(parsedItems);

            var areInStock = _itemCatalogService.ItemsAreInStock(convertedItems);

            return new SalesTotalCalculationResult
            {
                CalculationResultCode = CalculateTotalCalculationResultCode(areInStock),
                CalculatedAmount = areInStock ?
                (double?)_itemAmountCalculatorService.CalculateTotal(convertedItems) :
                null,
                PurchaseAssignment = convertedItems
            };
        }

        private static SalesCalculationResultCode CalculateTotalCalculationResultCode(bool areInStock)
        {
            return areInStock ?
                            SalesCalculationResultCode.AmountCalculated :
                            SalesCalculationResultCode.NotEnoughStock;
        }

        public SalesChangeCalculationResult CalculateChange(SalesChangeCalculationRequest calculationRequest)
        {
            if (calculationRequest is null)
            {
                throw new ArgumentNullException(nameof(calculationRequest));
            }

            var calculatedChange = calculationRequest.AmountPaid - calculationRequest.TotalAmount;

            return new SalesChangeCalculationResult
            {
                Change = calculatedChange,
                CalculationResultCode = CalculateChangeCalculationResultCode(calculatedChange)
            };
        }

        private static SalesChangeCalculationResultCode CalculateChangeCalculationResultCode(double calculatedChange)
        {
            if (calculatedChange > 0)
            {
                return SalesChangeCalculationResultCode.Change;
            }

            return calculatedChange == 0 ?
                SalesChangeCalculationResultCode.NoChange :
                SalesChangeCalculationResultCode.NotEnoughMoney;
        }
    }
}
