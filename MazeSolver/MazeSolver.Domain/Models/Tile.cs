using System.Collections.Generic;

namespace MazeSolver.Models
{
    public class Tile
    {
        public Point Location { get; set; }
        public TileType TileType { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Tile tile &&
                   EqualityComparer<Point>.Default.Equals(Location, tile.Location) &&
                   TileType == tile.TileType;
        }
    }
}