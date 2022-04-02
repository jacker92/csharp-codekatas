namespace TodoList.Console
{
    public interface IOutput
    {
        void WriteLine(string s);
        void WriteError(string v);
    }
}