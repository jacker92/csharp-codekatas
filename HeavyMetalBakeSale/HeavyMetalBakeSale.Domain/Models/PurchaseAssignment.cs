using HeavyMetalBakeSale.Domain.Models.DTO;

namespace HeavyMetalBakeSale.Domain.Models
{
    public class PurchaseAssignment
    {
        public CatalogStockItemDTO CatalogItem { get; set; }
        public int AmountToPurchase { get; set; }
    }
}
