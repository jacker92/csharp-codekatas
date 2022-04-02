using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TodoList.Domain.Tests
{
    public class TodoListTests
    {
        private readonly Mock<ITodoListRepository> _repository;
        private readonly TodoList _todoList;

        public TodoListTests()
        {
            _repository = new Mock<ITodoListRepository>();
            _todoList = new TodoList(_repository.Object);
        }

        [Fact]
        public void Add_ShouldThrowArgumentNullException_WithNullTodoItem()
        {
            Assert.Throws<ArgumentNullException>(() => _todoList.Add(null));
        }

        [Fact]
        public void Add_ShouldThrowArgumentException_WithTodoItemThatHasNonexistingParent()
        {
            var item = new TodoItem
            {
                ParentId = Guid.NewGuid(),
                Task = "todo"
            };

            var exception = Assert.Throws<ArgumentException>(() => _todoList.Add(item));
            Assert.Equal("Parent task invalid", exception.Message);
        }

        [Fact]
        public void Add_ShouldThrowArgumentException_WithTodoItemThatHasEmptyTaskName()
        {
            var item = new TodoItem
            {
            };

            var exception = Assert.Throws<ArgumentException>(() => _todoList.Add(item));
            Assert.Equal("Task name null or empty", exception.Message);
        }

        [Fact]
        public void Add_ShouldWork_WithTodoItemThatHasExistingParent()
        {
            var item = new TodoItem
            {
                Task = "parent"
            };

            _todoList.Add(item);

            var item2 = new TodoItem
            {
                Task = "Child",
                ParentId = item.Id
            };

            _todoList.Add(item2);
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
            var result = _todoList.GetById(item.Id);

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
            _repository.Verify(x => x.Save(It.Is<IEnumerable<TodoItem>>(x => x.Contains(item))), Times.Exactly(2));
        }

        [Fact]
        public void Complete_ShouldThrowInvalidOperationException_IfParentTaskIsTriedToComplete_ButItHasChildTasksNotCompleted()
        {
            var item = new TodoItem
            {
                Task = "parent"
            };

            _todoList.Add(item);

            var item2 = new TodoItem
            {
                Task = "Child",
                ParentId = item.Id
            };

            _todoList.Add(item2);

            var exception = Assert.Throws<InvalidOperationException>(() => _todoList.Complete(item.Id));
            Assert.Equal("You cannot complete parent task as it has open child tasks", exception.Message);
        }
    }
}