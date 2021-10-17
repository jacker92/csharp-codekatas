namespace MarsRover.Domain.Models
{
    public class MovementCalculationArgs
    {
        public Command Command { get; set; }
        public Orientation Orientation { get; set; }
        public Point CurrentLocation { get; set; }
        public Grid Grid { get; set; }
    }
}

