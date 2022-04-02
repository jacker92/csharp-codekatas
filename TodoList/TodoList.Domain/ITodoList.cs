
namespace TodoList.Domain
{
    public interface ITodoList
    {
        void Add(TodoItem item);
        void Complete(Guid guid);
        IEnumerable<TodoItem> GetAll();
        IEnumerable<TodoItem> GetAll(TodoItemStatus status);
        TodoItem GetById(Guid guid);
    }
}