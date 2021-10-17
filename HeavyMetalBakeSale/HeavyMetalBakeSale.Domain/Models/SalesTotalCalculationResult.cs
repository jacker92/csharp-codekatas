using System.Collections.Generic;

namespace HeavyMetalBakeSale.Domain.Models
{
    public class SalesTotalCalculationResult
    {
        public SalesCalculationResultCode CalculationResultCode { get; set; }
        public double? CalculatedAmount { get; set; }
        public IEnumerable<PurchaseAssignment> PurchaseAssignment { get; set; }
    }
}