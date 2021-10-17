using MazeSolver.Builders;
using MazeSolver.Models;
using MazeSolver.Models.MazeWalkers;
using MazeSolver.Services;
using MazeSolver.Utils;
using System.Collections.Generic;
using System.Text;

namespace MazeSolver
{
    public class Application
    {
        private readonly IMazeWalkerBuilder _mazeWalkerBuilder;
        private readonly IMazeBuilder _mazeBuilder;
        private readonly IScreen _screen;

        public Application(IMazeWalkerBuilder mazeWalkerBuilder, IScreen screen, IMazeBuilder mazeBuilder)
        {
            _mazeWalkerBuilder = mazeWalkerBuilder;
            _screen = screen;
            _mazeBuilder = mazeBuilder;
        }

        public void Run(MazeWalkerType mazeWalkerType, int mazeNumber, bool showPathOnMap = false)
        {
            var maze = _mazeBuilder.Build(mazeNumber);
            var entity = _mazeWalkerBuilder.Build(mazeWalkerType, maze);
            var shortestPath = entity.GetShortestPath();

            ShowShortestPath(shortestPath);

            if (showPathOnMap)
            {
                ShowPathOnMap(shortestPath, maze);
            }
        }

        private void ShowPathOnMap(Stack<Point> shortestPath, IMazeGrid maze)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < maze.Height; i++)
            {
                builder.Clear();
                for (int j = 0; j < maze.Grid[i].Length; j++)
                {
                    var current = new Point(j, i);

                    if (maze.Finish.Equals(current))
                    {
                        builder.Append($"{MazeCharacters.FinishPoint} ");
                    }
                    else if (shortestPath.Contains(current))
                    {
                        builder.Append($"{MazeCharacters.Path} ");
                    }
                    else if (maze.StartPosition.Equals(current))
                    {
                        builder.Append($"{MazeCharacters.StartingPoint} ");
                    }
                    else if (maze.Grid[i][j])
                    {
                        builder.Append($"{MazeCharacters.Floor} ");
                    }
                    else
                    {
                        builder.Append($"{MazeCharacters.Wall} ");
                    }
                }

                _screen.WriteOutput(builder.ToString().Trim());
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