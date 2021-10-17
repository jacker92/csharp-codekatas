using HeavyMetalBakeSale.Domain.Models;
using System.Collections.Generic;

namespace HeavyMetalBakeSale.Domain.Services
{
    public interface IItemCatalogService
    {
        bool ItemsAreInStock(IEnumerable<PurchaseAssignment> stockItems);
    }
}
