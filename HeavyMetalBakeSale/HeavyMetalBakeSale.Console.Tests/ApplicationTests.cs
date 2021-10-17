using HeavyMetalBakeSale.Domain.Exceptions;
using HeavyMetalBakeSale.Domain.Models;
using HeavyMetalBakeSale.Domain.Services;
using Moq;
using System;
using System.Globalization;
using Xunit;

namespace HeavyMetalBakeSale.Console.Tests
{
    public class ApplicationTests
    {
        private readonly Mock<ISalesCalculatorService> _salesCalculatorService;
        private readonly Mock<IDisplay> _display;
        private readonly Application _application;

        public ApplicationTests()
        {
            _salesCalculatorService = new Mock<ISalesCalculatorService>();
            _display = new Mock<IDisplay>();
            _application = new Application(_salesCalculatorService.Object, _display.Object);
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullSalesCalculationService()
        {
            Assert.Throws<ArgumentNullException>(() => new Application(null, _display.Object));
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullDisplay()
        {
            Assert.Throws<ArgumentNullException>(() => new Application(_salesCalculatorService.Object, null));
        }

        [Fact]
        public void Start_ShouldEndCycle_IfCalculationResultIsNotEnoughStock()
        {
            var input = "B, M";
            var amountPaid = "10.00";

            _display.SetupSequence(x => x.AskInput())
                .Returns(input)
                .Returns(amountPaid);

            _salesCalculatorService.Setup(x => x.CalculateTotal(It.IsAny<SalesTotalCalculationRequest>()))
                .Returns(new SalesTotalCalculationResult
                {
                    CalculatedAmount = 5,
                    CalculationResultCode = SalesCalculationResultCode.NotEnoughStock
                });

            _application.Start(true);

            _salesCalculatorService.Verify(x =>
                 x.CalculateChange(It.IsAny<SalesChangeCalculationRequest>()), Times.Never);
        }

        [Fact]
        public void Start_ShouldEndCycle_IfChangeCalculationResultIsNotEnoughMoney()
        {
            var input = "B, M";
            var amountPaid = "4.00";

            _display.SetupSequence(x => x.AskInput())
                .Returns(input)
                .Returns(amountPaid);

            _salesCalculatorService.Setup(x => x.CalculateTotal(It.IsAny<SalesTotalCalculationRequest>()))
                .Returns(new SalesTotalCalculationResult
                {
                    CalculatedAmount = 5
                });

            _salesCalculatorService.Setup(x => x.CalculateChange(It.IsAny<SalesChangeCalculationRequest>()))
                .Returns(new SalesChangeCalculationResult
                {
                    CalculationResultCode = SalesChangeCalculationResultCode.NotEnoughMoney
                });

            _application.Start(true);

            _salesCalculatorService.Verify(x =>
            x.CalculateTotal(It.Is<SalesTotalCalculationRequest>(x => x.Request.Equals(input))));

            _salesCalculatorService.Verify(x =>
           x.CalculateChange(It.IsAny<SalesChangeCalculationRequest>()), Times.Once);
        }

        [Fact]
        public void Start_ShouldCatchException_IfInputIsInInvalidFormat()
        {
            var input = "B, M";
            var amountPaid = "4.00";

            _display.SetupSequence(x => x.AskInput())
                .Returns(input)
                .Returns(amountPaid)
                .Returns(amountPaid);

            _salesCalculatorService.SetupSequence(x => x.CalculateTotal(It.IsAny<SalesTotalCalculationRequest>()))
                .Throws(new InvalidSalesCalculationRequestFormatException(input))
                .Returns(new SalesTotalCalculationResult
                {
                    CalculatedAmount = 5
                });

            _salesCalculatorService.SetupSequence(x => x.CalculateChange(It.IsAny<SalesChangeCalculationRequest>()))
              .Returns(new SalesChangeCalculationResult());

            _application.Start(true);
        }

        [Fact]
        public void Start_ShouldCatchExceptions_IfRequestedItemNotFound()
        {
            var input = "B, M";
            var amountPaid = "4.00";

            _display.SetupSequence(x => x.AskInput())
                .Returns(input)
                .Returns(amountPaid)
                .Returns(amountPaid);

            _salesCalculatorService.SetupSequence(x => x.CalculateTotal(It.IsAny<SalesTotalCalculationRequest>()))
                .Throws(new CatalogItemMappingNotFoundException("A"))
                .Returns(new SalesTotalCalculationResult
                {
                    CalculatedAmount = 5
                });

            _salesCalculatorService.SetupSequence(x => x.CalculateChange(It.IsAny<SalesChangeCalculationRequest>()))
                .Returns(new SalesChangeCalculationResult());

            _application.Start(true);
        }

        [Fact]
        public void Start_ShouldCatchExceptions_IfCalculateChageFormatIsInvalid()
        {
            var input = "B, M";
            var amountPaid = "4.00";

            _display.SetupSequence(x => x.AskInput())
                .Returns(input)
                .Returns(amountPaid)
                .Returns(amountPaid)
                .Returns(amountPaid);

            _salesCalculatorService.SetupSequence(x => x.CalculateTotal(It.IsAny<SalesTotalCalculationRequest>()))
                .Returns(new SalesTotalCalculationResult
                {
                    CalculatedAmount = 5
                });

            _salesCalculatorService.SetupSequence(x => x.CalculateChange(It.IsAny<SalesChangeCalculationRequest>()))
                .Throws(new FormatException())
                .Returns(new SalesChangeCalculationResult());

            _application.Start(true);
        }

        [Fact]
        public void Start_ShouldWork()
        {
            var input = "B, M";
            var amountPaid = "10.00";

            _display.SetupSequence(x => x.AskInput())
                .Returns(input)
                .Returns(amountPaid);

            _salesCalculatorService.Setup(x => x.CalculateTotal(It.IsAny<SalesTotalCalculationRequest>()))
                .Returns(new SalesTotalCalculationResult
                {
                    CalculatedAmount = 5
                });

            _salesCalculatorService.Setup(x => x.CalculateChange(
                It.Is<SalesChangeCalculationRequest>(x =>
                x.TotalAmount == 5 && x.AmountPaid == double.Parse(amountPaid, CultureInfo.InvariantCulture))))
                .Returns(new SalesChangeCalculationResult
                {
                    Change = 5
                });

            _application.Start(true);

            _salesCalculatorService.Verify(x =>
            x.CalculateTotal(It.Is<SalesTotalCalculationRequest>(x => x.Request.Equals(input))));
        }
    }
}
