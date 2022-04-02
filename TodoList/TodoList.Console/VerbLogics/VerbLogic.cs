namespace TodoList.Console.VerbLogics
{
    public abstract class VerbLogic<T> where T : class
    {
        public abstract int Run(T options);
    }
}