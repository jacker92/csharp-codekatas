using MarsRover.Domain.Services;
using System;

namespace MarsRover.Domain.Models
{
    public class MarsRover
    {
        private readonly IMovementCalculatorService _movementCalculatorService;
        private readonly CommandParserService _commandParserService;

        private readonly Grid _grid;

        public MarsRover(Point startingPoint, Orientation orientation, Grid grid)
        {
            Location = startingPoint ?? throw new ArgumentNullException(nameof(startingPoint));
            Orientation = orientation;
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            _movementCalculatorService = new MovementCalculatorService();
            _commandParserService = new CommandParserService();
        }

        public Point Location { get; private set; }
        public Orientation Orientation { get; private set; }

        public MovementResults Move(string moveInstructions)
        {
            if (string.IsNullOrWhiteSpace(moveInstructions))
            {
                throw new ArgumentException($"'{nameof(moveInstructions)}' cannot be null or whitespace.", nameof(moveInstructions));
            }

            return ExecuteMoves(moveInstructions);
        }

        private MovementResults ExecuteMoves(string moveInstructions)
        {
            var movementResults = new MovementResults();

            foreach (var command in _commandParserService.Parse(moveInstructions))
            {
                var movementCalculationArgs = new MovementCalculationArgs
                {
                    Command = command,
                    Orientation = Orientation,
                    CurrentLocation = Location,
                    Grid = _grid
                };
                var movementStatus = DelegateMoveBehaviour(command, movementCalculationArgs);
                movementResults.Movements.Add(new MovementResult { Status = movementStatus });
            }

            return movementResults;
        }

        private MovementStatus DelegateMoveBehaviour(Command command, MovementCalculationArgs movementCalculationArgs)
        {
            var movementStatus = MovementStatus.Ok;

            switch (command)
            {
                case Command.Forward:
                case Command.Backward:
                    var location = _movementCalculatorService.CalculateNewLocation(movementCalculationArgs);
                    if (location == Location)
                    {
                        movementStatus = MovementStatus.ObstacleEncountered;
                    }
                    Location = location;
                    break;
                case Command.Left:
                case Command.Right:
                    Orientation = _movementCalculatorService.CalculateNewOrientation(movementCalculationArgs);
                    break;
            }

            return movementStatus;
        }
    }
}

