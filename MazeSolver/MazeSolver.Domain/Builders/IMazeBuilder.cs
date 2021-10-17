using MazeSolver.Models;

namespace MazeSolver.Builders
{
    public interface IMazeBuilder
    {
        IMazeGrid Build(int mazeNumber);
    }
}