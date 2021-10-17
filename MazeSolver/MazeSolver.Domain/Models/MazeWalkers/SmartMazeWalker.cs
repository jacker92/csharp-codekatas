using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeSolver.Models.MazeWalkers
{
    public class SmartMazeWalker : BaseMazeWalker
    {
        private Location _location;

        public SmartMazeWalker(IMazeGrid mazeGrid) : base(mazeGrid)
        {
        }

        public override Stack<Point> GetShortestPath()
        {
            ComputePath();

            return CreateShortestPath();
        }

        private void ComputePath()
        {
            var start = new Location { Point = _mazeGrid.StartPosition };
            var openList = new List<Location>
            {
                start
            };
            var closedList = new List<Location>();

            while (openList.Any())
            {
                var lowest = openList.Min(l => l.F);
                _location = openList.First(l => l.F == lowest);

                CurrentPosition = _location.Point;

                // add the current square to the closed list
                closedList.Add(_location);

                // remove it from the open list
                openList.Remove(_location);

                if (AtFinish())
                {
                    break;
                }

                ComputeAdjacentSquares(openList, closedList);
            }
        }

        private static int ComputeHScore(Point source, Point target)
        {
            return Math.Abs(target.X - source.X) + Math.Abs(target.Y - source.Y);
        }

        private void ComputeAdjacentSquares(List<Location> openList, List<Location> closedList)
        {
            var target = new Location { Point = _mazeGrid.Finish };

            foreach (var adjacentSquare in GetWalkableAdjacentSquares(_location.Point))
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.Point.Equals(adjacentSquare.Point)) != null)
                {
                    continue;
                }

                // if it's not in the open list...
                if (openList.FirstOrDefault(l => l.Point.Equals(adjacentSquare.Point)) == null)
                {
                    adjacentSquare.H = ComputeHScore(adjacentSquare.Point, target.Point);
                    adjacentSquare.G = _location.G + 1;
                    adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                    adjacentSquare.Parent = _location;
                    openList.Add(adjacentSquare);
                }
            }
        }

        private Stack<Point> CreateShortestPath()
        {
            var actualPath = new Stack<Point>();
            while (_location != null && !_location.Point.Equals(_mazeGrid.StartPosition))
            {
                actualPath.Push(_location.Point);
                _location = _location.Parent;
            }

            return actualPath;
        }

        private List<Location> GetWalkableAdjacentSquares(Point current)
        {
            var proposedLocations = new List<Location>()
            {
                new Location { Point = new Point(current.X, current.Y - 1) },
                new Location { Point = new Point(current.X, current.Y + 1) },
                new Location { Point = new Point(current.X - 1, current.Y) },
                new Location { Point = new Point(current.X + 1, current.Y) }
            }.Where(x => x.Point.X >= 0 && x.Point.Y >= 0 &&
                         x.Point.X <= _mazeGrid.Width - 1 && x.Point.Y <= _mazeGrid.Height - 1 &&
                         !x.Point.Equals(_mazeGrid.StartPosition));

            return proposedLocations.Where(
                l => _mazeGrid.Grid[l.Point.Y][l.Point.X] || _mazeGrid.Finish.Equals(l.Point)).ToList();
        }
    }
}