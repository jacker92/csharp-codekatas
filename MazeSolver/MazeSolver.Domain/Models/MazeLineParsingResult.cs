namespace MazeSolver.Models
{
    public class MazeLineParsingResult
    {
        public Point Start { get; internal set; }
        public Point Finish { get; internal set; }
        public bool[][] Grid { get; internal set; }
    }
}