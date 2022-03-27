namespace TodoList.Domain
{
    public class TodoList
    {
        public void Add(object p)
        {
            if (p is null)
            {
                throw new ArgumentNullException(nameof(p));
            }

            throw new NotImplementedException();
        }
    }
}