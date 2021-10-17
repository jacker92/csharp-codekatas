using System;

namespace HeavyMetalBakeSale.Domain.Exceptions
{

    public class InvalidSalesCalculationRequestException : Exception
    {
        public InvalidSalesCalculationRequestException(string message) : base(message)
        {
        }
    }

}
