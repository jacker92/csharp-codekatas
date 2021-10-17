using HeavyMetalBakeSale.Domain.Models;

namespace HeavyMetalBakeSale.Domain.Services
{
    public interface ISalesCalculatorService
    {
        SalesChangeCalculationResult CalculateChange(SalesChangeCalculationRequest calculationRequest);
        SalesTotalCalculationResult CalculateTotal(SalesTotalCalculationRequest calculationRequest);
    }
}