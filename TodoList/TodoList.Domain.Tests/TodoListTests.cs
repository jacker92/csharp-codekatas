using System;
using System.Linq;
using Xunit;

namespace TodoList.Domain.Tests
{
    public class TodoListTests
    {
        [Fact]
        public void Add_ShouldThrowArgumentNullException_WithNullTodoItem()
        {
            var todolist = new TodoList();
            Assert.Throws<ArgumentNullException>(() => todolist.Add(null));
        }

        [Fact]
        public void Items_ShouldContainOneItem_AfterOneTodoItemIsAdded()
        {
            var todolist = new TodoList();
            var item = new TodoItem();
            todolist.Add(item);
            var result = todolist.Items.Single();

            Assert.Equal(item, result);
        }

        [Fact]
        public void Items_ShouldBeEmpty()
        {
            var todolist = new TodoList();
            Assert.Empty(todolist.Items);
        }
    }
}