using MazeSolver.Models;
using System.Collections.Generic;

namespace MazeSolver.Services
{
    public interface IScreenDisplayer
    {
        void ShowPathVisualization(IMazeGrid maze, Stack<Point> shortestPath);
        void ShowShortestPathInstructions(Stack<Point> shortestPath);
    }
}