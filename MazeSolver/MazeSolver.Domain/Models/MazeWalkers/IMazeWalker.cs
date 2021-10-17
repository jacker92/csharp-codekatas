using System.Collections.Generic;

namespace MazeSolver.Models.MazeWalkers
{
    public interface IMazeWalker
    {
        Stack<Point> GetShortestPath();
        bool AtFinish();
    }
}