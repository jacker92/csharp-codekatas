using HeavyMetalBakeSale.Domain.Models;
using HeavyMetalBakeSale.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeavyMetalBakeSale.Domain.Services
{

    public class ItemCatalogService : IItemCatalogService
    {
        private readonly ICatalogStockItemRepository _itemRepository;

        public ItemCatalogService(ICatalogStockItemRepository itemRepository)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        }

        public bool ItemsAreInStock(IEnumerable<PurchaseAssignment> purchaseOrderRequest)
        {
            if (purchaseOrderRequest is null)
            {
                throw new ArgumentNullException(nameof(purchaseOrderRequest));
            }

            if (!purchaseOrderRequest.Any())
            {
                throw new ArgumentException("Collection does not contain any items.", nameof(purchaseOrderRequest));
            }

            foreach (var item in purchaseOrderRequest)
            {
                var amountInStock = _itemRepository.Get(item.CatalogItem.ID);
                if (amountInStock.AmountInStock < item.AmountToPurchase)
                {
                    return false;
                }
            }

            return true;

        }

        public void CommitPurchase(IList<PurchaseAssignment> orderRequests)
        {
            if (orderRequests is null)
            {
                throw new ArgumentNullException(nameof(orderRequests));
            }

            foreach (var request in orderRequests)
            {
                var item = _itemRepository.Get(request.CatalogItem.ID);
                var updateRequest = new UpdateCatalogStockItemRequest
                {
                    CatalogStockItemID = request.CatalogItem.ID,
                    AmountInStock = item.AmountInStock - request.AmountToPurchase
                };

                _itemRepository.Update(updateRequest);
            }
        }
    }
}
