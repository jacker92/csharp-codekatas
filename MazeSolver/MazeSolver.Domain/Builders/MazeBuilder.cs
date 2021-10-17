using MazeSolver.Models;
using MazeSolver.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeSolver.Builders
{
    public class RandomMazeBuilder : IMazeBuilder
    {
        public IMazeGrid Build(int mazeNumber)
        {
            var width = 100;
            var tiles = new Tile[width][];
            for (int i = 0; i < width; i++)
            {
                tiles[i] = new Tile[100];
                for (int j = 0; j < 100; j++)
                {
                    tiles[i][j] = new Tile { TileType = TileType.Wall, Location = new Point(j, i) };
                }
            }

            var visitedCells = 0;
            var totalCells = width * width;

            tiles[0][0].TileType = TileType.Start;
            var currentCell = tiles[0][0];

            var cells = new Stack<Tile>();
            var random = new Random();

            while (visitedCells < totalCells)
            {
                // get a list of the neighboring cells with all 4 walls intact
                var adjacentCells = GetNeighborsWithWalls(currentCell, tiles);
                // test if a cell like this exists
                if (adjacentCells.Count > 0)
                {
                    // yes, choose one of them, and knock down the wall between it and the current cell
                    var randomCellNumber = random.Next(0, adjacentCells.Count());
                    var randomCell = adjacentCells[randomCellNumber];

                    tiles[randomCell.Location.Y][randomCell.Location.X].TileType = TileType.Floor;
                    cells.Push(currentCell); // push the current cell onto the stack
                    currentCell = randomCell; // make the random neighbor the new current cell
                    visitedCells++; // increment the # of cells visited
                }
                else // No adjacent cells that haven't been visited, go back to the previous cell
                {
                    currentCell = cells.Pop();
                }

                if (currentCell.Location.X == width - 1 ||
                    currentCell.Location.Y == width - 1)
                {
                    break;
                }
            }
            return new MazeGrid(tiles, new Point(0, 0), currentCell.Location);
        }

        private List<Tile> GetNeighborsWithWalls(Tile currentCell, Tile[][] tiles)
        {
            var proposedLocations = new List<Point>()
            {
                  new Point(currentCell.Location.X, currentCell.Location.Y - 1),
                  new Point(currentCell.Location.X, currentCell.Location.Y + 1),
                  new Point(currentCell.Location.X -1, currentCell.Location.Y),
                  new Point(currentCell.Location.X +1, currentCell.Location.Y)
            };

            return proposedLocations
                .Where(x =>
                 !MazeHelpers.IsOutOfBounds(new Point(x.X, x.Y), tiles) &&
                 tiles[x.Y][x.X].TileType == TileType.Wall)
                .Select(x => tiles[x.Y][x.X]).ToList();
        }
    }

    public class MazeBuilder : IMazeBuilder
    {
        private readonly IRawMazeReader _rawMazeReader;
        private readonly IMazeLineParser _mazeLineParser;

        public MazeBuilder(IRawMazeReader rawMazeReader, IMazeLineParser mazeLineParser)
        {
            _rawMazeReader = rawMazeReader ?? throw new ArgumentNullException(nameof(rawMazeReader));
            _mazeLineParser = mazeLineParser ?? throw new ArgumentNullException(nameof(mazeLineParser));
        }

        public IMazeGrid Build(int mazeNumber)
        {
            var lines = _rawMazeReader.Read(mazeNumber);
            var result = _mazeLineParser.Parse(lines);

            if (result.Start == null) throw new Exception("Maze should have a start position set.");
            if (result.Finish == null) throw new Exception("Maze should have a finish position set.");

            return new MazeGrid(result.Grid, result.Start, result.Finish);
        }
    }
}