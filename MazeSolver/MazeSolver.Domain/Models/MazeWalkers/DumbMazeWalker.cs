using System.Collections.Generic;

namespace MazeSolver.Models.MazeWalkers
{
    public class DumbMazeWalker : BaseMazeWalker
    {
        private Orientation _orientation;

        public DumbMazeWalker(IMazeGrid mazeGrid) : base(mazeGrid)
        {
            _orientation = Orientation.South;
        }

        public bool CanSeeLeftTurning()
        {
            var pointToOurLeft = new Point(CurrentPosition.X, CurrentPosition.Y);

            switch (_orientation)
            {
                case Orientation.North:
                    pointToOurLeft.X -= 1;
                    break;
                case Orientation.South:
                    pointToOurLeft.X += 1;
                    break;
                case Orientation.East:
                    pointToOurLeft.Y -= 1;
                    break;
                case Orientation.West:
                    pointToOurLeft.Y += 1;
                    break;
            }

            if (_mazeGrid.IsOutOfBounds(pointToOurLeft))
            {
                return false;
            }

            return _mazeGrid.Grid[pointToOurLeft.Y][pointToOurLeft.X].TileType != TileType.Wall;
        }

        public override Stack<Point> GetShortestPath()
        {
            bool endOfMazeReached = false;

            var path = new Stack<Point>();

            while (!endOfMazeReached)
            {
                var couldMoveForward = MoveForward();

                if (!couldMoveForward)
                {
                    TurnRight();
                }
                else
                {
                    if (CanSeeLeftTurning())
                    {
                        TurnLeft();
                    }
                }

                endOfMazeReached = AtFinish();
                path.Push(CurrentPosition);
            }
            return path;

        }

        public void TurnRight()
        {
            switch (_orientation)
            {
                case Orientation.North:
                    _orientation = Orientation.East;
                    break;
                case Orientation.East:
                    _orientation = Orientation.South;
                    break;
                case Orientation.South:
                    _orientation = Orientation.West;
                    break;
                case Orientation.West:
                    _orientation = Orientation.North;
                    break;
            }
        }

        public void TurnLeft()
        {
            switch (_orientation)
            {
                case Orientation.North:
                    _orientation = Orientation.West;
                    break;
                case Orientation.West:
                    _orientation = Orientation.South;
                    break;
                case Orientation.South:
                    _orientation = Orientation.East;
                    break;
                case Orientation.East:
                    _orientation = Orientation.North;
                    break;
            }
        }

        public bool MoveForward()
        {
            var desiredPoint = new Point(CurrentPosition.X, CurrentPosition.Y);

            switch (_orientation)
            {
                case Orientation.North:
                    desiredPoint.Y -= 1;
                    break;
                case Orientation.South:
                    desiredPoint.Y += 1;
                    break;
                case Orientation.East:
                    desiredPoint.X += 1;
                    break;
                case Orientation.West:
                    desiredPoint.X -= 1;
                    break;
            }

            var isOutOfBounds = _mazeGrid.IsOutOfBounds(desiredPoint);

            if (isOutOfBounds)
            {
                return false;
            }

            var location = _mazeGrid.Grid[desiredPoint.Y][desiredPoint.X];

            var canMoveForward = location.TileType != TileType.Start &&
                                    location.TileType != TileType.Wall;

            if (canMoveForward) CurrentPosition = desiredPoint;
            return canMoveForward;
        }
    }
}