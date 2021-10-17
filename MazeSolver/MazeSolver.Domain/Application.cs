using MazeSolver.Builders;
using MazeSolver.Models.MazeWalkers;

namespace MazeSolver
{
    public class Application
    {
        private readonly IMazeWalkerBuilder _mazeWalkerBuilder;
        private readonly IMazeBuilder _mazeBuilder;
        private readonly IScreenDisplayer _screenDisplayer;

        public Application(IMazeWalkerBuilder mazeWalkerBuilder, IMazeBuilder mazeBuilder, IScreenDisplayer screenDisplayer)
        {
            _mazeWalkerBuilder = mazeWalkerBuilder;
            _mazeBuilder = mazeBuilder;
            _screenDisplayer = screenDisplayer;
        }

        public void Run(MazeWalkerType mazeWalkerType, int mazeNumber, bool showPathOnMap = false)
        {
            var maze = _mazeBuilder.Build(mazeNumber);
            var entity = _mazeWalkerBuilder.Build(mazeWalkerType, maze);
            var shortestPath = entity.GetShortestPath();

            _screenDisplayer.ShowShortestPathInstructions(shortestPath);

            if (showPathOnMap)
            {
                _screenDisplayer.ShowPathVisualization(maze, shortestPath);
            }
        }
    }
}