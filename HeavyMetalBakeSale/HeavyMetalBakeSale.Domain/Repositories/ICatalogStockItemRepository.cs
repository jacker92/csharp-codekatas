using HeavyMetalBakeSale.Domain.Models;
using System.Collections.Generic;

namespace HeavyMetalBakeSale.Domain.Repositories
{
    public interface ICatalogStockItemRepository
    {
        CatalogStockItem Get(int id);
        IList<CatalogStockItem> Get();
        CatalogStockItem Update(UpdateCatalogStockItemRequest request);
    }
}
