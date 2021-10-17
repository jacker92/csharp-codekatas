using MarsRover.Domain.Models;

namespace MarsRover.Domain.Services
{
    public class OutOfBoundsPointCalculatorService
    {
        public Point CalculateNewLocationIfOutOfBounds(int x, int y, Grid grid)
        {
            y = y < 0 ? grid.Height : y > grid.Height ? 0 : y;
            x = x < 0 ? grid.Width : x > grid.Width ? 0 : x;

            return new Point(x, y);
        }
    }
}

