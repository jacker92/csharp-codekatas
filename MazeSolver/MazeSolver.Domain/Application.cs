using MazeSolver.Builders;
using MazeSolver.Models;
using MazeSolver.Models.MazeWalkers;
using MazeSolver.Services;
using System.Collections.Generic;

namespace MazeSolver
{
    public class Application
    {
        private readonly IMazeWalkerBuilder _mazeWalkerBuilder;
        private readonly IMazeBuilder _mazeBuilder;
        private readonly IScreen _screen;

        public Application(IMazeWalkerBuilder mazeWalkerBuilder, IScreen screen)
        {
            _mazeWalkerBuilder = mazeWalkerBuilder;
            _screen = screen;
        }

        public void Run(MazeWalkerType mazeWalkerType, int mazeNumber, bool showPathOnMap = false)
        {
            var maze = _mazeBuilder.Build(mazeNumber);

            var entity = _mazeWalkerBuilder.Build(mazeWalkerType, mazeNumber);

            var shortestPath = entity.GetShortestPath();

            ShowShortestPath(shortestPath);

            if (showPathOnMap)
            {
                ShowPathOnMap(shortestPath);
            }
        }

        private void ShowShortestPath(Stack<Point> shortestPath)
        {
            foreach (var item in shortestPath)
            {
                _screen.WriteOutput(item.ToString());
            }

            _screen.WriteOutput("Reached end of maze! :)");
        }
    }
}