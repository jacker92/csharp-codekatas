using System;

namespace MazeSolver.Exceptions
{
    public class MazeInputFormatException : Exception
    {
        public MazeInputFormatException(char c) : base($"Maze input had invalid character: {c}")
        {
        }
    }
}