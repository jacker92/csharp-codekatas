using HeavyMetalBakeSale.Domain.Models;
using System.Collections.Generic;

namespace HeavyMetalBakeSale.Domain.Services
{
    public interface IPurchaseAssignmentConverterService
    {
        IList<PurchaseAssignment> ConvertItems(IList<ParsedSalesTotalCalculationRequestResult> items);
    }
}
