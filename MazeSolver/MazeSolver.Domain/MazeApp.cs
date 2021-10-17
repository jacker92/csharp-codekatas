using MazeSolver.Builders;
using MazeSolver.Models.MazeWalkers;
using MazeSolver.Services;

namespace MazeSolver
{
    public class MazeApp
    {
        public static void Main()
        {
            var screen = new Screen();
            var mazeReader = new RawMazeFileReader();
            var mazeLineParser = new MazeLineParser();
            var mazeBuilder = new RandomMazeBuilder();
            var mazeWalkerBuilder = new MazeWalkerBuilder(mazeBuilder);
            var mazeSolutionVisualizer = new MazeSolutionVisualizer();
            var screenDisplayer = new ScreenDisplayer(screen, mazeSolutionVisualizer);
            var mazeApp = new Application(mazeWalkerBuilder, mazeBuilder, screenDisplayer);

            mazeApp.Run(MazeWalkerType.SmartMazeWalker, 3, true);
            screen.ReadInput();
        }
    }
}