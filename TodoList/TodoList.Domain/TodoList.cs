using System.Collections;

namespace TodoList.Domain
{
    public class TodoList
    {
        public List<TodoItem> Items { get; } = new List<TodoItem>();

        public void Add(TodoItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            Items.Add(item);
        }

        public void Complete(Guid guid)
        {
            var item = GetById(guid);
            item.Status = TodoItemStatus.Complete;
        }

        public TodoItem GetById(Guid guid)
        {
            var item = Items.SingleOrDefault(x => x.Id == guid);

            if (item == null)
            {
                throw new TodoItemNotFoundException(guid);
            }

            return item;
        }
    }
}