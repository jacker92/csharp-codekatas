namespace TodoList.Console.VerbLogics
{
    public interface IVerbLogic<T> where T : class
    {
        int Run(T options);
    }
}