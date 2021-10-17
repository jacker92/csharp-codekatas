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
            var mazeBuilder = new MazeBuilder(mazeReader, mazeLineParser);
            var mazeWalkerBuilder = new MazeWalkerBuilder(mazeBuilder);
            var mazeApp = new Application(mazeWalkerBuilder, screen);

            mazeApp.Run(MazeWalkerType.SmartMazeWalker, 1);
            screen.ReadInput();
        }
    }
}