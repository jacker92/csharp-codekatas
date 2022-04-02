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

            if (string.IsNullOrWhiteSpace(item.Task))
            {
                throw new ArgumentException("Task name null or empty");
            }

            if (item.ParentId.HasValue && !_items.Any(x => x.Id == item.ParentId.Value))
            {
                throw new ArgumentException("Parent task invalid");
            }

            _items.Add(item);
        }

        public void Complete(Guid guid)
        {
            var item = GetById(guid);
            var childTasks = _items.Where(x => x.ParentId == guid);

            if (childTasks.Any(x => x.Status != TodoItemStatus.Complete))
            {
                throw new InvalidOperationException("You cannot complete parent task as it has open child tasks");
            }

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