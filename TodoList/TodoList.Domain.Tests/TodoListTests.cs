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
        public void GetById_ShouldThrowTodoItemNotFoundException_IfNoMatchingItemIsFound()
        {
            Assert.Throws<TodoItemNotFoundException>(() => _todoList.GetById(Guid.NewGuid()));
        }

        [Fact]
        public void GetById_ShouldReturnMatchingItem()
        {
            var item = new TodoItem()
            {
                Task = "Todo item",
                Date = DateTime.Now
            };

            _todoList.Add(item);
            var result =  _todoList.GetById(item.Id);

            Assert.Equal(item, result);

        }

        [Fact]
        public void GetAll_ShouldReturnOneItem_AfterOneTodoItemIsAdded()
        {
            var item = new TodoItem()
            {
                Task = "Todo item",
                Date = DateTime.Now
            };

            _todoList.Add(item);
            var result = _todoList.GetAll().Single();

            Assert.Equal(item, result);
        }

        [Fact]
        public void GetAll_ShouldReturnEmptyCollectionByDefault()
        {
            Assert.Empty(_todoList.GetAll());
        }

        [Fact]
        public void GetAll_WithStatus_ShouldReturnEmptyCollectionByDefault()
        {
            Assert.Empty(_todoList.GetAll(TodoItemStatus.Complete));
        }

        [Fact]
        public void GetAll_WithStatus_ShouldReturnOneItem_AfterOneTodoItemIsAdded_AndStatusMatches()
        {
            var item = new TodoItem()
            {
                Task = "Todo item",
                Date = DateTime.Now
            };

            _todoList.Add(item);
            var result = _todoList.GetAll(TodoItemStatus.Incomplete).Single();

            Assert.Equal(item, result);
        }

        [Fact]
        public void GetAll_WithStatus_ShouldReturnEmptyCollection_AfterOneTodoItemIsAdded_ButStatusDoesNotMatch()
        {
            var item = new TodoItem()
            {
                Task = "Todo item",
                Date = DateTime.Now
            };

            _todoList.Add(item);
            var result = _todoList.GetAll(TodoItemStatus.Complete);

            Assert.Empty(result);
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

            Assert.Equal(TodoItemStatus.Complete, item.Status);
        }
    }
}