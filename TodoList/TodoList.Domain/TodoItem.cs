namespace TodoList.Domain
{
    public class TodoItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Task { get; set; }
        public DateTime? Date { get; set; }
        public TodoItemStatus Status { get; set; } = TodoItemStatus.Incomplete;
        public Guid? ParentId { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is TodoItem item &&
                   Id.Equals(item.Id) &&
                   Task == item.Task &&
                   Date == item.Date &&
                   Status == item.Status &&
                   EqualityComparer<Guid?>.Default.Equals(ParentId, item.ParentId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Task, Date, Status, ParentId);
        }
    }
}