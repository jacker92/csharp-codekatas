using MarsRover.Domain.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace MarsRover.Domain.Tests
{
    public class GridTests
    {
        private Grid _grid;

        public GridTests()
        {
            _grid = new Grid(50, 50);
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullObstaclesList()
        {
            Assert.Throws<ArgumentNullException>(() => new Grid(50, 50, null));
        }

        [Theory]
        [InlineData(0, 50)]
        [InlineData(-1, 50)]
        [InlineData(50, 0)]
        [InlineData(50, -1)]
        public void ShouldThrowArgumentOutOfRangeException_IfHeightOrWidthIsZeroOrNegative(int height, int width)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Grid(height, width, new List<Point>()));
        }

        [Fact]
        public void HasObstacleOn_ShouldThrowArgumentNullException_WithNullPoint()
        {
            Assert.Throws<ArgumentNullException>(() => _grid.HasObstacleOn(null));
        }

        [Fact]
        public void HasObstacleOn_ShouldReturnFalse_WhenNoObstaclesAreDefined()
        {
            var result = _grid.HasObstacleOn(new Point(0, 0));
            Assert.False(result);
        }

        [Fact]
        public void HasObstacleOn_ShouldReturnFalse_WhenObstacleIsOnThePoint()
        {
            var obstacle = new Point(5, 5);

            _grid = new Grid(50, 50, new List<Point> { obstacle });
            var result = _grid.HasObstacleOn(obstacle);
            Assert.True(result);
        }
    }
}
