using MazeSolver.Exceptions;
using MazeSolver.Services;
using Xunit;

namespace MazeSolver.Domain.Tests
{
    public class MazeLineParserTests
    {
        private readonly MazeLineParser _mazeLineParser;

        public MazeLineParserTests()
        {
            _mazeLineParser = new MazeLineParser();
        }

        [Fact]
        public void ShouldThrowMazeInputFormatException_WithInvalidCharacters()
        {
            var exception = Assert.Throws<MazeInputFormatException>(() => _mazeLineParser.Parse(new string[] { "a" }));
            Assert.Equal("Maze input had invalid character: a", exception.Message);
        }
    }
}
