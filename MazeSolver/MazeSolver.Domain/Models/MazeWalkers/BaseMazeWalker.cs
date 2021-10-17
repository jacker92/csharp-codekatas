using System;
using System.Collections.Generic;

namespace MazeSolver.Models.MazeWalkers
{
    public abstract class BaseMazeWalker : IMazeWalker
    {
        protected readonly IMazeGrid _mazeGrid;

        public BaseMazeWalker(IMazeGrid mazeGrid)
        {
            _mazeGrid = mazeGrid ?? throw new ArgumentNullException(nameof(mazeGrid));
            CurrentPosition = _mazeGrid.StartPosition;
        }

        public Point CurrentPosition { get; protected set; }

        public bool AtFinish()
        {
            return CurrentPosition.Equals(_mazeGrid.Finish);
        }

        public abstract Stack<Point> GetShortestPath();
    }
}