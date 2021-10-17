using MazeSolver.Models;
using MazeSolver.Services;
using System;

namespace MazeSolver.Builders
{
    public class MazeBuilder : IMazeBuilder
    {
        private readonly IRawMazeReader _rawMazeReader;
        private readonly IMazeLineParser _mazeLineParser;

        public MazeBuilder(IRawMazeReader rawMazeReader, IMazeLineParser mazeLineParser)
        {
            _rawMazeReader = rawMazeReader ?? throw new ArgumentNullException(nameof(rawMazeReader));
            _mazeLineParser = mazeLineParser ?? throw new ArgumentNullException(nameof(mazeLineParser));
        }

        public MazeGrid Build(int mazeNumber)
        {
            var lines = _rawMazeReader.Read(mazeNumber);
            var result = _mazeLineParser.Parse(lines);

            if (result.Start == null) throw new Exception("Maze should have a start position set.");
            if (result.Finish == null) throw new Exception("Maze should have a finish position set.");

            return new MazeGrid(result.Grid, result.Start, result.Finish);
        }
    }
}