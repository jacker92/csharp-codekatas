using System.Collections;

namespace TodoList.Domain
{
    public class TodoList : ITodoList
    {
        private readonly List<TodoItem> _items;

        public TodoList()
        {
            _items = new List<TodoItem>();
        }

        public void Add(TodoItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _items.Add(item);
        }

        public void Complete(Guid guid)
        {
            var item = GetById(guid);
            item.Status = TodoItemStatus.Complete;
        }

        public TodoItem GetById(Guid guid)
        {
            var item = _items.SingleOrDefault(x => x.Id == guid);

            if (item == null)
            {
                throw new TodoItemNotFoundException(guid);
            }

            return item;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _items;
        }

        public IEnumerable<TodoItem> GetAll(TodoItemStatus status)
        {
            return _items.Where(x => x.Status == status);
        }
    }
}