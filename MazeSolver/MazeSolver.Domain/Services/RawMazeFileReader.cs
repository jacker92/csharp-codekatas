using System;
using System.IO;

namespace MazeSolver.Services
{
    public class RawMazeFileReader : IRawMazeReader
    {
        public string[] Read(int mazeNumber)
        {
            var mazeFilePath = @"MazeFiles\maze" + mazeNumber + ".txt";
            return new StreamReader(
                new FileStream(mazeFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                .ReadToEnd()
                .Replace(" ", "")
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        }
    }
}