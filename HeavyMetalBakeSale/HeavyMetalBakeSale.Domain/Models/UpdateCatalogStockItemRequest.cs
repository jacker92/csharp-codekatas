namespace HeavyMetalBakeSale.Domain.Models
{
    public class UpdateCatalogStockItemRequest
    {
        public int CatalogStockItemID { get; set; }
        public int AmountInStock { get; set; }
    }
}
