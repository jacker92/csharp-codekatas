using System.Collections.Generic;

namespace MazeSolver.Models
{
    public static class MazeHelpers
    {
        public static bool IsOutOfBounds(Point point, Tile[][] grid)
        {
            return grid.Length == 0 || point.Y < 0 || point.Y > grid.Length -1 ||
                point.X < 0 || point.X > grid[point.Y].Length - 1;
        }

        public static List<Point> ProposedLocations(Point point)
        {
            return new List<Point>()
            {
                  new Point(point.X, point.Y - 1),
                  new Point(point.X, point.Y + 1),
                  new Point(point.X -1, point.Y),
                  new Point(point.X +1, point.Y)
            };
        }
    }
}