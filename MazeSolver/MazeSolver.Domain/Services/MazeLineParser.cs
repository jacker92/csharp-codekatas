using MazeSolver.Exceptions;
using MazeSolver.Models;
using MazeSolver.Utils;

namespace MazeSolver.Services
{
    public class MazeLineParser : IMazeLineParser
    {
        public MazeLineParsingResult Parse(string[] lines)
        {
            var result = new MazeLineParsingResult
            {
                Grid = new bool[lines.Length][]
            };

            for (int currentRow = 0; currentRow < lines.Length; currentRow++)
            {
                string line = lines[currentRow];
                result.Grid[currentRow] = new bool[line.Length];

                for (int currentCol = 0; currentCol < line.Length; currentCol++)
                {
                    switch (line[currentCol])
                    {
                        case MazeCharacters.StartingPoint:
                            result.Start = new Point(currentCol, currentRow);
                            break;
                        case MazeCharacters.FinishPoint:
                            result.Finish = new Point(currentCol, currentRow);
                            break;
                        case MazeCharacters.Wall:
                        case MazeCharacters.Floor:
                            break;
                        default:
                            throw new MazeInputFormatException(line[currentCol]);
                    }

                    result.Grid[currentRow][currentCol] = !line[currentCol].Equals(MazeCharacters.Wall);
                }
            }

            return result;
        }
    }
}