namespace MazeSolver.Models
{
    public class Location
    {
        public Point Point { get; set; }
        public int F { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public Location Parent { get; set; }
    }
}