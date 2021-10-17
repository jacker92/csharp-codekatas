namespace MazeSolver.Services
{
    public interface IScreen
    {
        void WriteOutput(string output);
        string ReadInput();
    }
}