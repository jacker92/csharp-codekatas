namespace MazeSolver.Models
{

    public interface IMazeGrid
    {
        Point Finish { get; }
        Tile[][] Grid { get; }
        Point StartPosition { get; }
    }
}