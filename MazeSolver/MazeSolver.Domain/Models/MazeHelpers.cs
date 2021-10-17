namespace MazeSolver.Models
{
    public static class MazeHelpers
    {
        public static bool IsOutOfBounds(Point point, Tile[][] grid)
        {
             return grid.Length == 0 || point.Y < 0 || point.Y > grid.Length ||
                 point.X < 0 || point.X > grid[point.Y].Length;
        }
    }
}