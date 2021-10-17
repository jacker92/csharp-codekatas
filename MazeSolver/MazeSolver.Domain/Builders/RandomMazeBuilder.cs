using MazeSolver.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeSolver.Builders
{
    public class RandomMazeBuilder : IMazeBuilder
    {
        public IMazeGrid Build(int mazeNumber)
        {
            var random = new Random();
            var width = random.Next(50, 100);
            var tiles = new Tile[width][];
            for (int i = 0; i < width; i++)
            {
                tiles[i] = new Tile[width];
                for (int j = 0; j < width; j++)
                {
                    tiles[i][j] = new Tile { TileType = TileType.Wall, Location = new Point(j, i) };
                }
            }

            var visitedCells = 0;
            var totalCells = width * width;

            var startPosition = new Point(random.Next(0, 5), random.Next(0, 5));
            var currentCell = tiles[startPosition.Y][startPosition.X];
            tiles[startPosition.Y][startPosition.X].TileType = TileType.Start;

            var cells = new Stack<Tile>();

            while (visitedCells < totalCells/4)
            {
                // get a list of the neighboring cells with all 4 walls intact
                var adjacentCells = GetNeighborsWithWalls(currentCell.Location, tiles);
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
            }

       
            return new MazeGrid(tiles, startPosition, currentCell.Location);
        }

        private List<Tile> GetNeighborsWithWalls(Point currentCell, Tile[][] tiles)
        {
            return MazeHelpers.ProposedLocations(currentCell)
                .Where(x =>
                 !MazeHelpers.IsOutOfBounds(new Point(x.X, x.Y), tiles) &&
                 tiles[x.Y][x.X].TileType == TileType.Wall)
                .Select(x => tiles[x.Y][x.X]).ToList();
        }
    }
}