namespace HeavyMetalBakeSale.Domain.Exceptions
{
    public class CatalogItemMappingNotFoundException : InvalidSalesCalculationRequestException
    {
        private static readonly string _message = "Mapping for abbreviation {0} cannot be found.";

        public CatalogItemMappingNotFoundException(string abbreviation)
            : base(string.Format(_message, abbreviation))
        {
            Abbreviation = abbreviation;
        }

        public string Abbreviation { get; }
    }

}
