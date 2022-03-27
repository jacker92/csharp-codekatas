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
    }
}