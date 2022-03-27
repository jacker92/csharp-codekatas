
namespace TodoList.Domain
{
    public interface ITodoList
    {
        void Add(TodoItem item);
        void Complete(Guid guid);
        IEnumerable<TodoItem> GetAll();
        TodoItem GetById(Guid guid);
    }
}