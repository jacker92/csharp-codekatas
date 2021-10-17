using HeavyMetalBakeSale.Domain.Exceptions;
using HeavyMetalBakeSale.Domain.Models;
using System;
using System.Linq;

namespace HeavyMetalBakeSale.Domain.Services
{
    public class SalesCalculatorValidatorService : ISalesCalculatorValidatorService
    {
        public void ValidateSalesTotalCalculationRequest(SalesTotalCalculationRequest calculationRequest)
        {
            if (calculationRequest is null)
            {
                throw new ArgumentNullException(nameof(calculationRequest));
            }

            var request = calculationRequest.Request;

            if (request == null)
            {
                throw new InvalidSalesCalculationRequestFormatException(request);
            }

            var values = request.Split(',');
            var containsOnlyNonEmptyValues = values.All(x => !string.IsNullOrWhiteSpace(x));

            if (!containsOnlyNonEmptyValues)
            {
                throw new InvalidSalesCalculationRequestFormatException(request);
            }
        }
    }
}
