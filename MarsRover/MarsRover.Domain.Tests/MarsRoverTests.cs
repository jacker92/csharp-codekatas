using MarsRover.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MarsRover.Domain.Tests
{
    public class MarsRoverTests
    {
        private Models.MarsRover _marsRover;

        public MarsRoverTests()
        {
            _marsRover = new Models.MarsRover(new Point(0, 0), Orientation.North, new Grid(50, 50));
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullLocation()
        {
            Assert.Throws<ArgumentNullException>(() => new Models.MarsRover(null, Orientation.East, new Grid(50, 50)));
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullGrid()
        {
            Assert.Throws<ArgumentNullException>(() => new Models.MarsRover(new Point(0, 0), Orientation.East, null));
        }

        [Fact]
        public void Move_ShouldThrowArgumentException_WithWhitespaceString()
        {
            Assert.Throws<ArgumentException>(() => _marsRover.Move(" "));
        }

        [Theory]
        [InlineData("ff", 5, 7, Orientation.North)]
        [InlineData("bb", 5, 3, Orientation.North)]
        [InlineData("ff", 5, 3, Orientation.South)]
        [InlineData("bb", 5, 7, Orientation.South)]
        [InlineData("ff", 3, 5, Orientation.West)]
        [InlineData("bb", 7, 5, Orientation.West)]
        [InlineData("ff", 7, 5, Orientation.East)]
        [InlineData("bb", 3, 5, Orientation.East)]
        [InlineData("flf", 4, 6, Orientation.North)]
        [InlineData("frf", 6, 6, Orientation.North)]
        [InlineData("fflff", 7, 3, Orientation.South)]
        [InlineData("fflff", 7, 7, Orientation.East)]
        [InlineData("fflff", 3, 7, Orientation.North)]
        [InlineData("fflff", 3, 3, Orientation.West)]
        public void Move_ShouldMoveMarsRoverCorrectly(string instructions, int newX, int newY, Orientation facingDirection)
        {
            _marsRover = new Models.MarsRover(new Point(5, 5), facingDirection, new Grid(50, 50));
            _marsRover.Move(instructions);
            Assert.Equal(newX, _marsRover.Location.X);
            Assert.Equal(newY, _marsRover.Location.Y);
        }

        [Theory]
        [InlineData("ff", 0, 1, 0, 50, Orientation.North)]
        [InlineData("ff", 0, 49, 0, 0, Orientation.South)]
        [InlineData("ff", 1, 0, 50, 0, Orientation.East)]
        [InlineData("ff", 49, 0, 0, 0, Orientation.West)]
        public void Move_ShouldImplementWrappingCorrectly(string instructions, int newX, int newY, int startX, int startY, Orientation facingDirection)
        {
            _marsRover = new Models.MarsRover(new Point(startX, startY), facingDirection, new Grid(50, 50));
            var movementResult = _marsRover.Move(instructions);
            Assert.Equal(newX, _marsRover.Location.X);
            Assert.Equal(newY, _marsRover.Location.Y);
            Assert.Equal(MovementStatus.Ok, movementResult.Status);
        }

        [Fact]
        public void Move_ShouldNotMove_WhenObstacleIsEncountered()
        {
            var obstacle = new Point(0, 1);

            _marsRover = new Models.MarsRover(new Point(0, 0), Orientation.North, new Grid(50, 50, new List<Point> { obstacle }));

           var movementResults = _marsRover.Move("f");
            Assert.Single(movementResults.Movements);
            Assert.Equal(MovementStatus.ObstacleEncountered, movementResults.Status);
            Assert.Equal(0, _marsRover.Location.X);
            Assert.Equal(0, _marsRover.Location.Y);
        }
    }
}
