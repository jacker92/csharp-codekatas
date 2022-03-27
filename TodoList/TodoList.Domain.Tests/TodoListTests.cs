using System;
using System.Linq;
using Xunit;

namespace TodoList.Domain.Tests
{
    public class TodoListTests
    {
        private readonly TodoList _todoList;

        public TodoListTests()
        {
            _todoList = new TodoList();
        }

        [Fact]
        public void Add_ShouldThrowArgumentNullException_WithNullTodoItem()
        {
            Assert.Throws<ArgumentNullException>(() => _todoList.Add(null));
        }

        [Fact]
        public void Items_ShouldContainOneItem_AfterOneTodoItemIsAdded()
        {
            var item = new TodoItem()
            {
                Task = "Todo item",
                Date = DateTime.Now
            };

            _todoList.Add(item);
            var result = _todoList.Items.Single();

            Assert.Equal(item, result);
        }

        [Fact]
        public void Items_ShouldBeEmpty()
        {
            Assert.Empty(_todoList.Items);
        }

        [Fact]
        public void Complete_ShouldThrowTodoItemNotFoundException_IfItemIsNotFoundByGuid()
        {
            var guid = Guid.NewGuid();
            var exception = Assert.Throws<TodoItemNotFoundException>(() => _todoList.Complete(guid));
            Assert.Equal($"Todo item could not be found with id: {guid}", exception.Message);
        }

        [Fact]
        public void Complete_ShouldCompleteTask()
        {
            var item = new TodoItem()
            {
                Task = "Todo item",
                Date = DateTime.Now
            };

            _todoList.Add(item);
            _todoList.Complete(item.Id);

            var foundItem = _todoList.Items.Single();

            Assert.Equal(TodoItemStatus.Complete, foundItem.Status);
        }
    }
}