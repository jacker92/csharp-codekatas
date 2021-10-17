using HeavyMetalBakeSale.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeavyMetalBakeSale.Domain.Services
{
    public class ItemAmountCalculatorService : IItemAmountCalculatorService
    {
        public double CalculateTotal(IEnumerable<PurchaseAssignment> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.Sum(x => x.CatalogItem.Item.Price * x.AmountToPurchase);
        }
    }
}
