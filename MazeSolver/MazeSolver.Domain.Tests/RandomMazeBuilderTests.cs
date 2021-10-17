using MazeSolver.Builders;
using Xunit;

namespace MazeSolver.Domain.Tests
{
    public class RandomMazeBuilderTests
    {
        private readonly RandomMazeBuilder _randomMazeBuilder;

        public RandomMazeBuilderTests()
        {
            _randomMazeBuilder = new RandomMazeBuilder();
        }

        [Fact]
        public void ShouldWork()
        {
           var maze= _randomMazeBuilder.Build(1);
            Assert.NotNull(maze);
        }
    }
}
