namespace HeavyMetalBakeSale.Domain.Models.DTO
{
    public class CatalogStockItemDTO
    {
        public int ID { get; set; }
        public CatalogItemDTO Item { get; set; }
        public int AmountInStock { get; set; }
    }
}
