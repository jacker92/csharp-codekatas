namespace TodoList.Console
{
    public abstract class VerbLogic<T> where T : class
    {
        public abstract int Run(T options);
    }
}