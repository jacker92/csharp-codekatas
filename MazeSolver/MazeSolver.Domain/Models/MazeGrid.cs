namespace MazeSolver.Models
{
    public class MazeGrid : IMazeGrid
    {
        public MazeGrid(Tile[][] grid, Point start, Point finish)
        {
            Grid = grid;
            StartPosition = start;
            Finish = finish;
        }

        public Point StartPosition { get; }

        public Point Finish { get; }

        public Tile[][] Grid { get; }

        public bool IsOutOfBounds(Point point)
        {
            return point.Y < 0 || point.Y > Grid.Length ||
                 point.X < 0 || point.X > Grid[point.Y].Length;
        }
    }
}