using MazeSolver.Models;
using MazeSolver.Utils;
using System;
using System.Collections.Generic;

namespace MazeSolver.Services
{
    public class MazeSolutionVisualizer : IMazeSolutionVisualizer
    {
        public string CreateVisualization(IMazeGrid maze, Stack<Point> shortestPath)
        {
            var lines = new List<string>();
            for (int i = 0; i < maze.Height; i++)
            {
                var characters = new List<char>();
                for (int j = 0; j < maze.Grid[i].Length; j++)
                {
                    var current = new Point(j, i);

                    if (maze.Finish.Equals(current))
                    {
                        characters.Add(MazeCharacters.FinishPoint);
                    }
                    else if (shortestPath.Contains(current))
                    {
                        characters.Add(MazeCharacters.Path);
                    }
                    else if (maze.StartPosition.Equals(current))
                    {
                        characters.Add(MazeCharacters.StartingPoint);
                    }
                    else if (maze.Grid[i][j])
                    {
                        characters.Add(MazeCharacters.Floor);
                    }
                    else
                    {
                        characters.Add(MazeCharacters.Wall);
                    }
                }
                lines.Add(string.Join(" ", characters));
            }

            return string.Join(Environment.NewLine, lines);
        }
    }
}