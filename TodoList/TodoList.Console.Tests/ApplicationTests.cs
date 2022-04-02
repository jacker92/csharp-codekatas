using Moq;
using System;
using System.Collections.Generic;
using TodoList.Domain;
using Xunit;

namespace TodoList.Console.Tests
{
    public class ApplicationTests
    {
        private readonly Mock<IOutput> _output;
        private readonly Mock<ITodoList> _todoList;
        private readonly Application _application;

        public ApplicationTests()
        {
            _output = new Mock<IOutput>();
            _todoList = new Mock<ITodoList>();
            _application = new Application(_output.Object, _todoList.Object);
        }

        [Fact]
        public void Run_ShouldReturnInvalidArgumentsText_IfPassedInvalidArguments()
        {
            _application.Run(null);

            _output.Verify(x => x.WriteLine(Messages.InvalidArguments));
        }

        [Fact]
        public void Run_ShouldReturnInstructions_IfDashDashHelpIsGiven()
        {
            _application.Run(new string[] { "--help" });

            _output.Verify(x => x.WriteLine(It.Is<string>(x => x.Contains("© Microsoft Corporation. All rights reserved."))));
        }

        [Fact]
        public void Run_ShouldAddItemToTodoList()
        {
            _application.Run(new string[] { "task", "-t", "application", "-d", "12-12-2021" });

            _todoList.Verify(x => x.Add(It.Is<TodoItem>(x => x.Task == "application" && x.Date == DateTime.Parse("12-12-2021"))));
        }

        [Fact]
        public void Run_ShouldNotAddItemToTodoList_IfNameIsMissing()
        {
            _application.Run(new string[] { "task", "-d", "12-12-2021" });

            _todoList.Verify(x => x.Add(It.IsAny<TodoItem>()), Times.Never);
        }

        [Fact]
        public void Run_ShouldListTodoListItems()
        {
            var todoItem = new TodoItem { Task = "Complete Application", Date = DateTime.Parse("01-04-2018") };

            _todoList.Setup(x => x.GetAll())
                    .Returns(new List<TodoItem> { todoItem });

            _application.Run(new string[] { "list" });

            _todoList.Verify(x => x.GetAll(), Times.Once);

            var expected =
@$"Id: {todoItem.Id}
Task: Complete Application
Due: 01-04-2018
";

            _output.Verify(x => x.WriteLine(expected), Times.Once);
        }

    }
}