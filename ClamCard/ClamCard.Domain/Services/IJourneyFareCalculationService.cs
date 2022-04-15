using ClamCard.Domain.Models;

namespace ClamCard.Domain.Services
{
    public interface IJourneyFareCalculationService
    {
        JourneyFare Calculate(Journey journey);
    }
}