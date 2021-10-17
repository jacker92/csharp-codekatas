using HeavyMetalBakeSale.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeavyMetalBakeSale.Domain.Repositories
{
    public class CatalogStockItemRepository : ICatalogStockItemRepository
    {
        private List<CatalogStockItem> _items = new List<CatalogStockItem>
        {
            new CatalogStockItem { ID = 1, Item = new CatalogItem { ID = 1, Name = "Brownie", Price = .65}, AmountInStock = 48 },
            new CatalogStockItem { ID = 2, Item = new CatalogItem { ID = 2, Name = "Muffin", Price = 1.00}, AmountInStock = 36 },
            new CatalogStockItem { ID = 3,Item = new CatalogItem { ID = 3, Name = "Cake Pop", Price = 1.35}, AmountInStock = 24 },
            new CatalogStockItem { ID = 4, Item = new CatalogItem { ID = 4, Name = "Water", Price = 1.50}, AmountInStock = 30 }
        };

        public IList<CatalogStockItem> Get()
        {
            return _items;
        }

        public CatalogStockItem Get(int id)
        {
            return _items.FirstOrDefault(x => x.ID == id);
        }

        public CatalogStockItem Update(UpdateCatalogStockItemRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _items = _items.Select(x =>
            {
                if (x.ID == request.CatalogStockItemID)
                {
                    x.AmountInStock = request.AmountInStock < 0 ? 0 : request.AmountInStock;
                };
                return x;
            }).ToList();

            return Get(request.CatalogStockItemID);
        }
    }
}
