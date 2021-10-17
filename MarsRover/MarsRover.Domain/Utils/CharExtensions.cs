using MarsRover.Domain.Models;
using System;

namespace MarsRover.Domain.Utils
{
    public static class CharExtensions
    {
        public static Command ToCommand(this char directionChar)
        {
            return directionChar == 'f'
                ? Command.Forward
                : directionChar == 'b'
                ? Command.Backward
                : directionChar == 'l'
                ? Command.Left
                : directionChar == 'r'
                ? Command.Right
                : throw new Exception("Invalid command");
        }
    }
}
