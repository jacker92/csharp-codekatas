using MazeSolver.Models;
using MazeSolver.Models.MazeWalkers;

namespace MazeSolver.Builders
{
    public interface IMazeWalkerBuilder
    {
        IMazeWalker Build(MazeWalkerType mazeWalkerType, IMazeGrid mazeGrid);
    }
}