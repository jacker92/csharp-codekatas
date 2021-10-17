using MazeSolver.Models;

namespace MazeSolver.Services
{
    public interface IMazeLineParser
    {
        MazeLineParsingResult Parse(string[] lines);
    }
}