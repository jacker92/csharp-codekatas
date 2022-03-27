namespace TodoList.Domain
{
    public class TodoItemNotFoundException : Exception
    {
        public TodoItemNotFoundException(Guid guid) : base($"Todo item could not be found with id: {guid}")
        {

        }
    }
}