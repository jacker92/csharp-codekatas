namespace TodoList.Console
{
    public class Output : IOutput
    {
        public void WriteError(string error)
        {
            System.Console.Error.WriteLine(error);
        }

        public void WriteLine(string content)
        {
            System.Console.WriteLine(content);
        }

    }
}