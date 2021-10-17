using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Domain.Models
{
    public class Grid
    {
        public Grid(int height, int width) : this(height, width, new List<Point>()) { }

        public Grid(int height, int width, IList<Point> obstacles)
        {
            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Argument cannot be zero or negative");
            }

            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Argument cannot be zero or negative");
            }

            Height = height;
            Width = width;
            Obstacles = obstacles ?? throw new ArgumentNullException(nameof(obstacles));
        }

        public int Height { get; }
        public int Width { get; }
        public IList<Point> Obstacles { get; }

        public bool HasObstacleOn(Point point)
        {
            if (point is null)
            {
                throw new ArgumentNullException(nameof(point));
            }

            return Obstacles.Any(x => x.X == point.X && x.Y == point.Y);
        }
    }
}
