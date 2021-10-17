using MazeSolver.Models;
using System.Collections.Generic;

namespace MazeSolver
{
    public interface IScreenDisplayer
    {
        void ShowPathVisualization(IMazeGrid maze, Stack<Point> shortestPath);
        void ShowShortestPathInstructions(Stack<Point> shortestPath);
    }
}