using MarsRover.Domain.Models;

namespace MarsRover.Domain.Services
{
    public interface IMovementCalculatorService
    {
        Point CalculateNewLocation(MovementCalculationArgs args);
        Orientation CalculateNewOrientation(MovementCalculationArgs args);

    }
}

