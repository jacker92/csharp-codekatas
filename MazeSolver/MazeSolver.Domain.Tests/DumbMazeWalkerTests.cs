using MazeSolver.Models;
using MazeSolver.Models.MazeWalkers;
using Moq;
using System;
using Xunit;

namespace MazeSolver.Domain.Tests
{
    public class DumbMazeWalkerTests
    {
        private readonly Mock<IMazeGrid> _mazeGrid;
        private readonly DumbMazeWalker _dumbMazeWalker;

        public DumbMazeWalkerTests()
        {
            _mazeGrid = new Mock<IMazeGrid>();
            _mazeGrid.SetReturnsDefault(new Point(0, 0));
          
            _dumbMazeWalker = new DumbMazeWalker(_mazeGrid.Object);
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullMazeGrid()
        {
            Assert.Throws<ArgumentNullException>(() => new DumbMazeWalker(null));
        }

        [Fact]
        public void ShouldTurnLeftCorrectly_AndNotMoveWalker()
        {
            _dumbMazeWalker.TurnLeft();
            _dumbMazeWalker.TurnLeft();
            _dumbMazeWalker.TurnLeft();
            _dumbMazeWalker.TurnLeft();

            Assert.Equal(_mazeGrid.Object.StartPosition, _dumbMazeWalker.CurrentPosition);
        }

        [Fact]
        public void ShouldTurnRightCorrectly_AndNotMoveWalker()
        {
            _dumbMazeWalker.TurnRight();
            _dumbMazeWalker.TurnRight();
            _dumbMazeWalker.TurnRight();
            _dumbMazeWalker.TurnRight();

            Assert.Equal(_mazeGrid.Object.StartPosition, _dumbMazeWalker.CurrentPosition);
        }

        [Fact]
        public void CanSeeLeftTurning_ShouldWork()
        {
            var result = _dumbMazeWalker.CanSeeLeftTurning();

            Assert.False(result);
        }

        [Fact]
        public void CanSeeLeftTurning_ShouldWork_WithNorthOrientation()
        {
            _dumbMazeWalker.TurnRight();
            _dumbMazeWalker.TurnRight();

            var result = _dumbMazeWalker.CanSeeLeftTurning();

            Assert.False(result);
        }

        [Fact]
        public void MoveForward_ShouldWork_WithNorthOrientation()
        {
            _dumbMazeWalker.TurnRight();
            _dumbMazeWalker.TurnRight();

            var result = _dumbMazeWalker.MoveForward();

            Assert.False(result);
        }
    }
}
