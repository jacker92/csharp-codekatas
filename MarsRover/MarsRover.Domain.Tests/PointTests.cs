using MarsRover.Domain.Models;
using Xunit;

namespace MarsRover.Domain.Tests
{
    public class PointTests
    {
        private readonly Point _point;

        public PointTests()
        {
            _point = new Point(0, 0);
        }

        [Fact]
        public void X_ShouldBe0ByDefault()
        {
            Assert.Equal(0, _point.X);
        }

        [Fact]
        public void Y_ShouldBe0ByDefault()
        {
            Assert.Equal(0, _point.Y);
        }
    }
}
