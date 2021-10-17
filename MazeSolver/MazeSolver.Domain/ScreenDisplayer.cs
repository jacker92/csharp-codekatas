using MazeSolver.Models;
using MazeSolver.Services;
using System;
using System.Collections.Generic;

namespace MazeSolver
{
    public class ScreenDisplayer : IScreenDisplayer
    {
        private readonly IScreen _screen;
        private readonly IMazeSolutionVisualizer _mazeSolutionVisualizer;

        public ScreenDisplayer(IScreen screen, IMazeSolutionVisualizer mazeSolutionVisualizer)
        {
            _screen = screen ?? throw new ArgumentNullException(nameof(screen));
            _mazeSolutionVisualizer = mazeSolutionVisualizer ?? throw new ArgumentNullException(nameof(mazeSolutionVisualizer));
        }

        public void ShowShortestPathInstructions(Stack<Point> shortestPath)
        {
            foreach (var item in shortestPath)
            {
                _screen.WriteOutput(item.ToString());
            }

            _screen.WriteOutput("Reached end of maze! :)");
        }

        public void ShowPathVisualization(IMazeGrid maze, Stack<Point> shortestPath)
        {
            var visualization = _mazeSolutionVisualizer.CreateVisualization(maze, shortestPath);
            _screen.WriteOutput(visualization);
        }
    }
}