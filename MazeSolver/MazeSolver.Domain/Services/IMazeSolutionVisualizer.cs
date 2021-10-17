using MazeSolver.Models;
using System.Collections.Generic;

namespace MazeSolver.Services
{
    public interface IMazeSolutionVisualizer
    {
        string CreateVisualization(IMazeGrid maze, Stack<Point> shortestPath);
    }
}