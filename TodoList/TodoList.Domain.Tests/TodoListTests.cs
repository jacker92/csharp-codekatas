using System;
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
    }
}