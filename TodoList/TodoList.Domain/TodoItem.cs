namespace TodoList.Domain
{
    public class TodoItem
    {
        public string Task { get; set; }
        public DateTime Date { get; set; }
        public TodoItemStatus Status { get; set; } = TodoItemStatus.Incomplete;
    }
}