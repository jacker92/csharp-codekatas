using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRover.Domain.Models
{
    public class MovementResults
    {
        public MovementStatus Status => GetStatus();
        public IList<MovementResult> Movements { get; set; } = new List<MovementResult>();

        private MovementStatus GetStatus()
        {
            return Movements.Any(x => x.Status == MovementStatus.ObstacleEncountered)
                ? MovementStatus.ObstacleEncountered
                : MovementStatus.Ok;
        }
    }
}
