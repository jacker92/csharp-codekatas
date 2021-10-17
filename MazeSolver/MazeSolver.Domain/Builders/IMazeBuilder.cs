using MazeSolver.Models;

namespace MazeSolver.Builders
{
    public interface IMazeBuilder
    {
        MazeGrid Build(int mazeNumber);
    }
}