using MarsRover.Domain.Utils;
using System.Collections.Generic;

namespace MarsRover.Domain.Models
{
    public class CommandParserService
    {
        public IList<Command> Parse(string commandString)
        {
            var list = new List<Command>();

            foreach (var c in commandString.ToLower())
            {
                list.Add(c.ToCommand());
            }

            return list;
        }
    }
}

