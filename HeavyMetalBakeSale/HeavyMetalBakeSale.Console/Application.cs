using HeavyMetalBakeSale.Domain.Exceptions;
using HeavyMetalBakeSale.Domain.Models;
using HeavyMetalBakeSale.Domain.Services;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace HeavyMetalBakeSale.Console
{
    public class Application : IApplication
    {
        private readonly ISalesCalculatorService _salesCalculatorService;
        private readonly IDisplay _display;

        public Application(ISalesCalculatorService salesCalculatorService, IDisplay display)
        {
            _salesCalculatorService = salesCalculatorService ?? throw new ArgumentNullException(nameof(salesCalculatorService));
            _display = display ?? throw new ArgumentNullException(nameof(display));
        }

        [ExcludeFromCodeCoverage]
        public void Start(bool runOnlyOnce = false)
        {
            while (true)
            {
                RunCycle();
                _display.ShowOutput($"\n\n");
                if (runOnlyOnce)
                {
                    return;
                }
            }
        }

        private void RunCycle()
        {
            var result = CalculateTotal();

            _display.ShowOutput($"Total > ");

            if (result.CalculationResultCode == SalesCalculationResultCode.NotEnoughStock)
            {
                _display.ShowOutput($"Not enough stock");
                return;
            }

            _display.ShowOutput($"${result.CalculatedAmount.Value:N2}");

            var calculationResult = CalculateChange(result);

            _display.ShowOutput($"Change > ");

            if (calculationResult.CalculationResultCode == SalesChangeCalculationResultCode.NotEnoughMoney)
            {
                _display.ShowOutput($"Not enough money");
                return;
            }

            _display.ShowOutput($"{calculationResult.Change:N2}");
        }

        private SalesChangeCalculationResult CalculateChange(SalesTotalCalculationResult result)
        {
            SalesChangeCalculationResult calculationResult;
            while (true)
            {
                _display.ShowOutput("\nAmount Paid > $");
                var input = _display.AskInput();
                try
                {
                    var amountPaid = double.Parse(input, CultureInfo.InvariantCulture);
                    calculationResult = _salesCalculatorService.CalculateChange(new SalesChangeCalculationRequest
                    {
                        AmountPaid = amountPaid,
                        TotalAmount = result.CalculatedAmount.Value
                    });
                    break;
                }
                catch (FormatException)
                {
                    _display.ShowOutput("Please enter value in correct format");
                }

            }
            return calculationResult;
        }

        private SalesTotalCalculationResult CalculateTotal()
        {
            SalesTotalCalculationResult result;
            while (true)
            {
                _display.ShowOutput("Items to purchase > ");

                var input = _display.AskInput();

                var request = new SalesTotalCalculationRequest { Request = input };

                try
                {
                    result = _salesCalculatorService.CalculateTotal(request);
                    break;
                }
                catch (InvalidSalesCalculationRequestFormatException)
                {
                    _display.ShowOutput($"Request is not in valid format\n");
                }
                catch (CatalogItemMappingNotFoundException e)
                {
                    _display.ShowOutput($"Cannot find item: {e.Abbreviation}\n");
                }
            }

            return result;
        }
    }

}
