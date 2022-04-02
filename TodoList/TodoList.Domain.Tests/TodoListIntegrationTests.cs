using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TodoList.Domain.Tests
{
    public class TodoListIntegrationTests
    {
        private readonly TodoList _todoList;
        private readonly TodoListRepository _todoListRepository;

        public TodoListIntegrationTests()
        {
            _todoListRepository = new TodoListRepository();
            _todoList = new TodoList(_todoListRepository);
        }

        [Fact]
        public void Add_ShouldSaveTodoItem()
        {
            var item = new TodoItem
            {
                Task = "parent"
            };

            _todoList.Add(item);

            var todoList = new TodoList(_todoListRepository);

            var retrievedItem = todoList.GetById(item.Id);

            Assert.Equal(item, retrievedItem);
        }

        [Fact]
        public void RepositoryTest()
        {
            var content = @"[{'Id':'7110f95a-fc6e-42b7-99c6-3a3064757a90','Task':'parent','Date':'0001-01-01T00:00:00','Status':0,'ParentId':null}]";
            var result = JsonConvert.DeserializeObject<IEnumerable<TodoItem>>(content);
            Assert.Equal(Guid.Parse("7110f95a-fc6e-42b7-99c6-3a3064757a90"), result.First().Id);
        }
    }
}
