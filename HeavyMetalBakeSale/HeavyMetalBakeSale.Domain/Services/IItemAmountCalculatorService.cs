using HeavyMetalBakeSale.Domain.Models;
using System.Collections.Generic;

namespace HeavyMetalBakeSale.Domain.Services
{
    public interface IItemAmountCalculatorService
    {
        double CalculateTotal(IEnumerable<PurchaseAssignment> items);
    }
}
