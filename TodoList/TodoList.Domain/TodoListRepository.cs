using Newtonsoft.Json;

namespace TodoList.Domain
{
    public class TodoListRepository : ITodoListRepository
    {
        private const string _filepath = "objs.json";

        public void Save(IEnumerable<TodoItem> items)
        {
            var contentsToWriteToFile = JsonConvert.SerializeObject(items);
            using (var writer = new StreamWriter(_filepath))
            {
                writer.Write(contentsToWriteToFile);
            }
        }
        public IEnumerable<TodoItem> GetAll()
        {
            if (!File.Exists(_filepath))
            {
                return Enumerable.Empty<TodoItem>();
            }

            using (var reader = new StreamReader(_filepath))
            {
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<IEnumerable<TodoItem>>(fileContents)!;
            }
        }
    }
}