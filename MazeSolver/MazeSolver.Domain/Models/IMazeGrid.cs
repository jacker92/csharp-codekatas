namespace MazeSolver.Models
{
    public interface IMazeGrid
    {
        int Width { get; }
        int Height { get; }
        Point Finish { get; }
        bool[][] Grid { get; }
        Point StartPosition { get; }
        bool IsOutOfBounds(Point point);
    }
}