namespace MarsRover.Domain.Models
{
    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; } = 0;
        public int Y { get; } = 0;
    }
}
