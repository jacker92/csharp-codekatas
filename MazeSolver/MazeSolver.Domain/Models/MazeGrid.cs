namespace MazeSolver.Models
{
    public class MazeGrid : IMazeGrid
    {
        // +--------> +ve X
        // |
        // |
        // |
        // |
        // v
        // +ve Y

        public MazeGrid(bool[][] grid, Point start, Point finish)
        {
            Grid = grid;
            StartPosition = start;
            Finish = finish;
        }

        public Point StartPosition { get; }

        public Point Finish { get; }

        public bool[][] Grid { get; }

        public int Width => Grid.Length;

        public int Height => Grid.Length;

        public bool IsOutOfBounds(Point point)
        {
            return point.Y < 0 || point.Y > Grid.Length ||
                 point.X < 0 || point.X > Grid.Length;
        }
    }
}