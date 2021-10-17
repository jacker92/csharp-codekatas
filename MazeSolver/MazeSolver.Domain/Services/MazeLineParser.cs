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
                Grid = new Tile[lines.Length][]
            };

            for (int currentRow = 0; currentRow < lines.Length; currentRow++)
            {
                string line = lines[currentRow];
                result.Grid[currentRow] = new Tile[line.Length];

                for (int currentCol = 0; currentCol < line.Length; currentCol++)
                {
                    TileType? tileType;
                    switch (line[currentCol])
                    {
                        case MazeCharacters.StartingPoint:
                            result.Start = new Point(currentCol, currentRow);
                            tileType = TileType.Start;
                            break;
                        case MazeCharacters.FinishPoint:
                            result.Finish = new Point(currentCol, currentRow);
                            tileType = TileType.End;
                            break;
                        case MazeCharacters.Wall:
                            tileType = TileType.Wall;
                            break;
                        case MazeCharacters.Floor:
                            tileType = TileType.Floor;
                            break;
                        default:
                            throw new MazeInputFormatException(line[currentCol]);
                    }

                    result.Grid[currentRow][currentCol] = new Tile
                    {
                        TileType = tileType.Value
                    };

                }
            }

            return result;
        }
    }
}