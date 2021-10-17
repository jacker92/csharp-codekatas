using MazeSolver.Builders;
using MazeSolver.Models.MazeWalkers;
using MazeSolver.Services;
using Moq;
using System;
using Xunit;

namespace MazeSolver.Domain.Tests
{

    public class ApplicationTests
    {
        private readonly Application _application;
        private readonly Mock<IScreen> _screen;

        public ApplicationTests()
        {
            _screen = new Mock<IScreen>();
            var mazeReader = new RawMazeFileReader();
            var mazeLineParser = new MazeLineParser();
            var mazeBuilder = new MazeBuilder(mazeReader, mazeLineParser);
            var mazeWalkerBuilder = new MazeWalkerBuilder(mazeBuilder);
            var mazeSolutionVisualizer = new MazeSolutionVisualizer();
            var screenDisplayer = new ScreenDisplayer(_screen.Object, mazeSolutionVisualizer);
            _application = new Application(mazeWalkerBuilder, mazeBuilder, screenDisplayer);
        }

        [Fact]
        public void DumbMazeWalker_ShouldWork_WithMazeNumber1()
        {
            _application.Run(MazeWalkerType.DumbMazeWalker, 1);

            _screen.Verify(x => x.WriteOutput("Point(1, 1)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(2, 1)"), Times.Exactly(2));
            _screen.Verify(x => x.WriteOutput("Point(3, 1)"), Times.Exactly(2));
            _screen.Verify(x => x.WriteOutput("Point(4, 1)"), Times.Exactly(3));
            _screen.Verify(x => x.WriteOutput("Point(2, 2)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(2, 3)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(3, 3)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(4, 3)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(5, 3)"), Times.Once);

            _screen.Verify(x => x.WriteOutput("Reached end of maze! :)"), Times.Once);
            _screen.VerifyNoOtherCalls();
        }

        [Fact]
        public void SmartMazeWalker_ShouldWork_WithMazeNumber1()
        {
            _application.Run(MazeWalkerType.SmartMazeWalker, 1, true);

            _screen.Verify(x => x.WriteOutput("Point(1, 1)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(2, 1)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(2, 2)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(2, 3)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(3, 3)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(4, 3)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(5, 3)"), Times.Once);

            _screen.Verify(x => x.WriteOutput("Reached end of maze! :)"), Times.Once);

            _screen.Verify(x => x.WriteOutput(string.Join(
                Environment.NewLine,
                "# S # # # #",
                "# * * . . #",
                "# # * # # #",
                "# . * * * F",
                "# # # . # #",
                "# # # # # #")));

            _screen.VerifyNoOtherCalls();
        }

        [Fact]
        public void SmartMazeWalker_ShouldWork_WithMazeNumber2()
        {
            _application.Run(MazeWalkerType.SmartMazeWalker, 2);

            _screen.Verify(x => x.WriteOutput("Point(3, 2)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(3, 3)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(2, 3)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(1, 3)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(1, 4)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(1, 5)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(1, 6)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(1, 7)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(2, 7)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(3, 7)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(3, 6)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(3, 5)"), Times.Once);

            _screen.Verify(x => x.WriteOutput("Reached end of maze! :)"), Times.Once);
            _screen.VerifyNoOtherCalls();
        }

        [Fact]
        public void SmartMazeWalker_ShouldWork_WithMazeNumber3()
        {
            _application.Run(MazeWalkerType.SmartMazeWalker, 3);

            _screen.Verify(x => x.WriteOutput("Point(3, 2)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(3, 3)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(4, 3)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(5, 3)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(5, 4)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(5, 5)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(5, 6)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(5, 7)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(5, 8)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(4, 8)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(4, 9)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(3, 9)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(2, 9)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(2, 10)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(2, 11)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(1, 11)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(1, 12)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(1, 13)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(1, 14)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(1, 15)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(2, 15)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(3, 15)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(3, 14)"), Times.Once);
            _screen.Verify(x => x.WriteOutput("Point(3, 13)"), Times.Once);

            _screen.Verify(x => x.WriteOutput("Reached end of maze! :)"), Times.Once);
            _screen.VerifyNoOtherCalls();
        }
    }
}
