using MazeSolver.Models;
using System.Collections.Generic;

namespace MazeSolver
{
    public interface IMazeSolutionVisualizer
    {
        string CreateVisualization(IMazeGrid maze, Stack<Point> shortestPath);
    }
}