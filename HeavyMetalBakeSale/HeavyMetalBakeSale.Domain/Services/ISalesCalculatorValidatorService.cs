using HeavyMetalBakeSale.Domain.Models;

namespace HeavyMetalBakeSale.Domain.Services
{
    public interface ISalesCalculatorValidatorService
    {
        void ValidateSalesTotalCalculationRequest(SalesTotalCalculationRequest calculationRequest);
    }
}
