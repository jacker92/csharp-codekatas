using MazeSolver.Models;
using MazeSolver.Models.MazeWalkers;
using System;

namespace MazeSolver.Builders
{
    public class MazeWalkerBuilder : IMazeWalkerBuilder
    {
        private readonly IMazeBuilder _mazeBuilder;

        public MazeWalkerBuilder(IMazeBuilder mazeBuilder)
        {
            _mazeBuilder = mazeBuilder ?? throw new ArgumentNullException(nameof(mazeBuilder));
        }

        public IMazeWalker Build(MazeWalkerType mazeWalkerType, IMazeGrid mazeGrid)
        {

            IMazeWalker mazeWalker = null;
            switch (mazeWalkerType)
            {
                case MazeWalkerType.DumbMazeWalker:
                    mazeWalker = new DumbMazeWalker(mazeGrid);
                    break;
                case MazeWalkerType.SmartMazeWalker:
                    mazeWalker = new SmartMazeWalker(mazeGrid);
                    break;
            }

            return mazeWalker;
        }
    }
}