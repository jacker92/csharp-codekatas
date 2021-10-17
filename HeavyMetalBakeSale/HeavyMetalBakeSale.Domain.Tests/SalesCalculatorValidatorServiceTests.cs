using HeavyMetalBakeSale.Domain.Exceptions;
using HeavyMetalBakeSale.Domain.Models;
using HeavyMetalBakeSale.Domain.Services;
using System;
using Xunit;

namespace HeavyMetalBakeSale.Domain.Tests
{
    public class SalesCalculatorValidatorServiceTests
    {
        private readonly SalesCalculatorValidatorService _salesCalculatorValidatorService;

        public SalesCalculatorValidatorServiceTests()
        {
            _salesCalculatorValidatorService = new SalesCalculatorValidatorService();
        }

        [Fact]
        public void ValidateSalesTotalCalculationRequest_ShouldThrowArgumentNullException_WithNullSalesTotalCalculationRequest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            _salesCalculatorValidatorService.ValidateSalesTotalCalculationRequest(null));
        }

        [Fact]
        public void ValidateSalesTotalCalculationRequest_ShouldThrowInvalidSalesCalculationRequestFormatException_WithNullRequest()
        {
            var request = new SalesTotalCalculationRequest
            {
                Request = null
            };

            Assert.Throws<InvalidSalesCalculationRequestFormatException>(() =>
            _salesCalculatorValidatorService.ValidateSalesTotalCalculationRequest(request));
        }

        [Fact]
        public void ValidateSalesTotalCalculationRequest_ShouldThrowInvalidSalesCalculationRequestFormatException_WithNullOrWhitespaceRequest()
        {
            var request = new SalesTotalCalculationRequest
            {
                Request = " "
            };

            Assert.Throws<InvalidSalesCalculationRequestFormatException>(() =>
            _salesCalculatorValidatorService.ValidateSalesTotalCalculationRequest(request));
        }

        [Fact]
        public void ValidateSalesTotalCalculationRequest_ShouldThrowInvalidSalesCalculationRequestFormatException_WithInvalidRequestString()
        {
            var request = new SalesTotalCalculationRequest { Request = "B,C,," };

            var exception = Assert.Throws<InvalidSalesCalculationRequestFormatException>(
                    () => _salesCalculatorValidatorService.ValidateSalesTotalCalculationRequest(request));

            Assert.Equal($"RequestString {request.Request} is not in correct format", exception.Message);
        }

        [Fact]
        public void ValidateSalesTotalCalculationRequest_ShouldWork_WithValidInput()
        {
            var request = new SalesTotalCalculationRequest { Request = "B,C" };
            _salesCalculatorValidatorService.ValidateSalesTotalCalculationRequest(request);
        }
    }
}
