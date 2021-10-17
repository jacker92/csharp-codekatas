namespace HeavyMetalBakeSale.Domain.Exceptions
{
    public class InvalidSalesCalculationRequestFormatException : InvalidSalesCalculationRequestException
    {
        private static readonly string _message = "RequestString {0} is not in correct format";

        public InvalidSalesCalculationRequestFormatException(string requestString)
            : base(string.Format(_message, requestString))
        {
        }
    }

}
