using MazeSolver.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeSolver.Builders
{
    public class RandomMazeBuilder : IMazeBuilder
    {
        private readonly Random _random;

        public RandomMazeBuilder()
        {
            _random = new Random();
        }

        public IMazeGrid Build(int mazeNumber)
        {
            var width = _random.Next(50, 100);
            Tile[][] tiles = InitializeTiles(width);

            var totalCells = width * width;

            var startPosition = new Point(_random.Next(1, 5), _random.Next(1, 5));
            var currentCell = tiles[startPosition.Y][startPosition.X];
            tiles[startPosition.Y][startPosition.X].TileType = TileType.Start;

            TraverseTiles(tiles, totalCells, ref currentCell);

            return new MazeGrid(tiles, startPosition, currentCell.Location);
        }

        private void TraverseTiles(Tile[][] tiles, int totalCells, ref Tile currentCell)
        {
            var cells = new Stack<Tile>();
            var visitedCells = 0;
            var alreadyProcessedCells = new List<Tile>();
            while (visitedCells < totalCells / 2)
            {
                int required = GetRequiredAmountOfWalls(currentCell, alreadyProcessedCells);

                // get a list of the neighboring cells with all 4 walls intact
                var adjacentCells = GetNeighborsWithWalls(currentCell.Location, tiles, required);
                // test if a cell like this exists
                if (adjacentCells.Count > 0)
                {
                    // yes, choose one of them, and knock down the wall between it and the current cell
                    var randomCellNumber = _random.Next(0, adjacentCells.Count());

                    var randomCell = adjacentCells[randomCellNumber];

                    tiles[randomCell.Location.Y][randomCell.Location.X].TileType = TileType.Floor;
                    cells.Push(currentCell); // push the current cell onto the stack
                    currentCell = randomCell; // make the random neighbor the new current cell
                    visitedCells++; // increment the # of cells visited

                    alreadyProcessedCells.Add(currentCell);
                }
                else // No adjacent cells that haven't been visited, go back to the previous cell
                {
                    currentCell = cells.Pop();
                }
            }
        }

        private static int GetRequiredAmountOfWalls(Tile currentCell, List<Tile> alreadyProcessedCells)
        {
            return alreadyProcessedCells.Contains(currentCell) ? 2 : 3;
        }

        private static Tile[][] InitializeTiles(int width)
        {
            var tiles = new Tile[width][];
            for (int i = 0; i < width; i++)
            {
                tiles[i] = new Tile[width];
                for (int j = 0; j < width; j++)
                {
                    tiles[i][j] = new Tile { TileType = TileType.Wall, Location = new Point(j, i) };
                }
            }

            return tiles;
        }

        private List<Tile> GetNeighborsWithWalls(Point currentCell, Tile[][] tiles, int requiredAmount)
        {
            var proposed = MazeHelpers.ProposedLocations(currentCell);

            var res = proposed.Where(x =>
            {
                return !MazeHelpers.IsOutOfBounds(new Point(x.X, x.Y), tiles) &&
                tiles[x.Y][x.X].TileType == TileType.Wall;
            }).ToList();


            return res.Count() >= requiredAmount ? res.Select(x => tiles[x.Y][x.X]).ToList() : new List<Tile>();
        }
    }
}