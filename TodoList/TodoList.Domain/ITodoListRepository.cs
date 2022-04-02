
namespace TodoList.Domain
{
    public interface ITodoListRepository
    {
        IEnumerable<TodoItem> GetAll();
        void Save(IEnumerable<TodoItem> items);
    }
}