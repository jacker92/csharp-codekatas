using MarsRover.Domain.Models;
using MarsRover.Domain.Utils;

namespace MarsRover.Domain.Services
{
    public class MovementCalculatorService : IMovementCalculatorService
    {
        private readonly OutOfBoundsPointCalculatorService _pointCalculator;

        public MovementCalculatorService()
        {
            _pointCalculator = new OutOfBoundsPointCalculatorService();
        }

        public Orientation CalculateNewOrientation(MovementCalculationArgs args)
        {
            var last = args.Orientation.GetLast();
            var first = args.Orientation.GetFirst();

            int newOrientation = (int)args.Orientation;

            Orientation? calculatedOrientation = null;

            switch (args.Command)
            {
                case Command.Left:
                    newOrientation--;
                    calculatedOrientation = newOrientation < (int)first ? last : (Orientation)newOrientation;
                    break;
                case Command.Right:
                    newOrientation++;
                    calculatedOrientation = newOrientation > (int)last ? first : (Orientation)newOrientation;
                    break;
            }

            return calculatedOrientation.Value;
        }

        public Point CalculateNewLocation(MovementCalculationArgs args)
        {
            var x = args.CurrentLocation.X;
            var y = args.CurrentLocation.Y;

            switch (args.Orientation)
            {
                case Orientation.North:
                    y += args.Command == Command.Forward ? 1 : -1;
                    break;
                case Orientation.South:
                    y += args.Command == Command.Forward ? -1 : 1;
                    break;
                case Orientation.East:
                    x += args.Command == Command.Forward ? 1 : -1;
                    break;
                case Orientation.West:
                    x += args.Command == Command.Forward ? -1 : 1;
                    break;
            }

            var newLocation = _pointCalculator.CalculateNewLocationIfOutOfBounds(x, y, args.Grid);

            return args.Grid.HasObstacleOn(newLocation) ? args.CurrentLocation : newLocation;
        }
    }
}

