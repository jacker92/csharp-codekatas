namespace HeavyMetalBakeSale.Domain.Models
{

    public class CatalogStockItem
    {
        public int ID { get; set; }
        public CatalogItem Item { get; set; }
        public int AmountInStock { get; set; }
    }
}
